using System.Data;
using EmptyProjectASPNETCORE.Exception;
using MediatR;
using Npgsql;

namespace EmptyProjectASPNETCORE;

public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private NpgsqlTransaction? _npgsqlTransaction;
        private readonly IDbConnectionFactory<NpgsqlConnection>? _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private readonly IPublisher _publisher;

        public UnitOfWork(
            IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker, 
            IPublisher publisher)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
            _publisher = publisher;
        }

        public async ValueTask StartTransaction(CancellationToken token)
        {
            if (!(_npgsqlTransaction is null && _dbConnectionFactory is not null))
                return;
            var connection = await _dbConnectionFactory.CreateConnection(token);
            _npgsqlTransaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted, token);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            if (_npgsqlTransaction is null)
            {
                throw new NoActiveTransactionStartedException();
            }

            var domainEvents = new Queue<INotification>(
                _changeTracker.TrackedEntities
                    .SelectMany(x =>
                    {
                        if (x.DomainEvents is null)
                            return Enumerable.Empty<INotification>();
                        
                        var events = x.DomainEvents.ToList();
                        x.ClearDomainEvents();
                        return events;
                    }));
            
            while(domainEvents.TryDequeue(out var notification))
            {
                await _publisher.Publish(notification, cancellationToken);
            }

            await _npgsqlTransaction.CommitAsync(cancellationToken);
        }

        void IDisposable.Dispose()
        {
            _npgsqlTransaction?.Dispose();
            _dbConnectionFactory?.Dispose();
        }
    }