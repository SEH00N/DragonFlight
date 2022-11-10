const ws = require('ws');
const server = new ws.Server( { port : 3030 } );

const Enums = require('./Enums/Enums.js');
const Packet = require('./Classes/Packet.js').Packet;

const handlers = []; //handler list

handlers[Enums.Types.Room] = require('./Handlers/RoomHandler.js').handler;
handlers[Enums.Types.GameManager] = require('./Handlers/GameManagerHandler.js').handler;
handlers[Enums.Types.Interact] = require('./Handlers/InteractHandler.js').handler;

server.once('listening', () => {
    console.log('\x1b[33m%s\x1b[0m', `[ServerSystem] server opened on port ${server.options.port}`);
});

server.on('connection', (socket, req) => {
    console.log('\x1b[33m%s\x1b[0m', '[ServerSystem] client connected');

    socket.on('message', msg => {
        data = JSON.parse(msg.toString());
        var packet = new Packet(data.t, data.e, data.v);
        // console.log(data.t + " " + data.e);
        
        callback = handlers[packet.type][packet.event];
        // console.log(typeof(callback));

        if(typeof(callback) == 'function')
            callback(socket, packet);
    });

});
