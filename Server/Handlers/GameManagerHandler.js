const Enums = require('../Enums/Enums.js');
const Room = require('../Classes/Room.js').Room;
const Packet = require('../Classes/Packet.js').Packet;

const handler = [];

handler[Enums.GameManagerEvents.Ready] = function(socket, packet) {
    if (socket.ready == undefined)
        socket.ready = false;

    socket.ready = !(socket.ready);
    packet.value = socket.ready;

    global.rooms[socket.roomId].players.forEach(soc => {
        if(soc != socket)
            soc.send(packet.asPacket());
    });
}

handler[Enums.GameManagerEvents.Start] = function(socket, packet) {
    ready2Start = true;
    global.rooms[socket.roomId].players.forEach(soc => ready2Start &= soc.ready );

    if(ready2Start)
    {
        global.rooms[socket.roomId].players.forEach(soc => {
            packet.value = (global.rooms[socket.roomId].host == soc) ? 0 : 1;
            soc.send(packet.asPacket());
        });
    }
}

exports.handler = handler;