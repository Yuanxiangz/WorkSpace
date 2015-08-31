var http = require('http');
var port = process.env.port || 1000;
console.log('Server start and listen localhost:'+ port);
http.createServer(function (req, res) {
    res.writeHead(200, { 'Content-Type': 'text/plain' });
    res.end('Hello World\n');
}).listen(port);