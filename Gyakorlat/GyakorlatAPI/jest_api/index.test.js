const http = require('http');
const { hasUncaughtExceptionCaptureCallback } = require('process');

describe('suite', ()=>{
    it('add works', async () => {
        
        const result = await httpGet();

        expect(result).toBe("[]");
    })

    it('Post works', (done) => {
        
        const postData = JSON.stringify({"Name": "Pempo", "Difficulty": 1, "FoodType": 1});

        const options = {
            hostname: 'localhost',
            port: 5000,
            path: '/api/food',
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
              'Content-Length': Buffer.byteLength(postData)
            }
          };

          const req = http.request(options, (res) => {
            console.log(`STATUS: ${res.statusCode}`);
            console.log(`HEADERS: ${JSON.stringify(res.headers)}`);
            res.setEncoding('utf8');
            res.on('data', (chunk) => {
              console.log(`BODY: ${chunk}`);
            });
            res.on('end', () => {
              console.log('No more data in response.');
              done();
            });
          });
          
          req.on('error', (e) => {
            console.error(`problem with request: ${e.message}`);
          });
          
          // Write data to request body
          req.write(postData);
          req.end();
    })
})

async function httpGet() {

    return new Promise((resolve, reject) => {
        const options = {
            hostname: 'localhost',
            port: 5000,
            path: '/api/food',
            method: 'GET',
        };
        
        const req = http.request(options, (res) => {
            // console.log(`STATUS: ${res.statusCode}`);
            // console.log(`HEADERS: ${JSON.stringify(res.headers)}`);
            res.setEncoding('utf8');
            let data = '';
            res.on('data', (chunk) => {
                data += chunk;
                // console.log(`BODY: ${chunk}`);
            });
            res.on('end', () => {
                // console.log('No more data in response.');
                resolve(data);
            });
        });
        
        req.on('error', (e) => {
            // console.error(`problem with request: ${e.message}`);
            reject(e);
        });
        
        // Write data to request body
        //req.write(JSON.);
        req.end();
    } )
}

