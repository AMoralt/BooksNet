import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    ext: {
        loadimpact: {
            projectID: 3610586,
            name: "Testing"
        }
    },
    stages: [
        { duration: '10s', target: 2000 }, 
        { duration: '1m', target: 2000 },
        { duration: '10s', target: 6000 }, //spike to 6000 users
        { duration: '3m', target: 6000 },
        { duration: '10s', target: 2000 },
        { duration: '3m', target: 2000 },
        { duration: '1m', target: 0 },
    ],
    thresholds: {
        http_req_duration: ['p(99)<1500'], // 99% of requests must complete below 1.5ss
    },
};

export default function () {
    const cars = ["9785171341671", "9785041575441", "9785171386535", "9785041549435","9785353102434","9785353102144","9785171480035"];
    
    const id = Math.floor(Math.random() * 7);
    
    let response = http.get(`http://localhost:5672/Book/${cars[id]}`);

    /*
    const isbn = Math.round( Math.random() * (9999999999 - 1000000000) + 1000000000);
    //console.log(isbn);
    let data = {
        title: 'test',
        genreId: 2,
        formatId: 2,
        authors: [
            1,3,2
        ],
        publisherId: 2,
        isbn: `${isbn}`,
        publicationdate: '2020',
        price: 220,
        quantity: 20 };
    let res = http.post('http://localhost:5672/Book', JSON.stringify(data), {
        headers: { 'Content-Type': 'application/json' },
    });
    */
    
    let success = check(response, {
        "statusCode is 200": (r) => r.json().statusCode === 200,
    });
    if(response.json().statusCode !== 200){
        console.log(response.json());
    }

    sleep(1);
}
