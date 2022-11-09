const Enums = require('../Enums/Enums.js');
const Room = require('../Classes/Room.js').Room;
const Packet = require('../Classes/Packet.js').Packet;

const handler = [];
const startInterval = 3;

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
    if(global.rooms[socket.roomId].players.length < 2)
        return;

    ready2Start = true;
    global.rooms[socket.roomId].players.forEach(soc => ready2Start &= soc.ready );

    if(ready2Start)
    {
        global.rooms[socket.roomId].players.forEach(soc => {
            soc.ready = false;
            packet.value = (global.rooms[socket.roomId].host == soc) ? 0 : 1;
            soc.send(packet.asPacket());
        });
    }
}

handler[Enums.GameManagerEvents.Fight] = function(socket, packet) {
    socket.ready2Fight = true;

    fight = true;
    global.rooms[socket.roomId].players.forEach(soc => fight &= soc.ready2Fight );

    if(fight)
    {
        setTimeout(() => {
            packet.event = Enums.GameManagerEvents.Fight;
            packet.value = "";
            
            global.rooms[socket.roomId].players.forEach(soc => {
                soc.send(packet.asPacket());
                soc.ready2Fight = false;
            });
        }, startInterval * 1000);
    }
}

exports.handler = handler;