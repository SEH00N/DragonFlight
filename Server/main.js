const ws = require('ws');
const server = new ws.Server( { port : 3031 } );

const handlers = []; //handler list

server.once('listening', () => {
    console.log(`[ServerSystem] server opened on port ${server.options.port}`);
});

server.on('connection', (socket, req) => {
    console.log('[ServerSystem] client connected');

    socket.on('message', msg => {
        packet = JSON.parse(msg);

        callback = handlers[packet.t][packet.y];

        if(typeof(callback) == 'function')
            callback(socket, packet);
    });
});
