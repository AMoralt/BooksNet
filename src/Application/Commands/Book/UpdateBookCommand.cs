﻿using MediatR;

namespace EmptyProjectASPNETCORE;

public record UpdateBookCommand(
    string? title,
    int? genreId,
    int? formatId,
    IEnumerable<int> authors,
    int? publisherId,
    string? isbn,
    string? publicationdate,
    int? price,
    int? quantity) : IRequest;