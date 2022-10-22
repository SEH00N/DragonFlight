const ws = require('ws');
const server = new ws.Server( { port : 3031 } );

const Enums = require('../Enums/Enums.js');

const handlers = []; //handler list

handlers[Enums.Types.Room] = require('../Handlers/RoomHandler.js').roomHandler;

server.once('listening', () => {
    console.log(`[ServerSystem] server opened on port ${server.options.port}`);
});

server.on('connection', (socket, req) => {
    console.log('[ServerSystem] client connected');

    socket.on('message', msg => {
        data = JSON.stringify(msg);
        var packet = new Packet(data.t, data.e, data.v);

        callback = handlers[packet.type][packet.event];

        if(typeof(callback) == 'function')
            callback(socket, packet);
    });
});
