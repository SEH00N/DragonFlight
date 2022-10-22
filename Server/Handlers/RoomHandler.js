const enums = require('../Enums/Enums.js');

const roomHandler = [];
let rooms = {};

roomHandler[enums.RoomEvents.Create] = function(socket, packet) {
    try {
        var room = new Room(socket, rooms);
        rooms[room.id] = room;
    
        socket.room = room;
    
        packet.value = true;
        socket.send(packet.asPacket());
    } catch {
        packet.value = false;
        socket.send(packet.asPacket());
    }
}

roomHandler[enums.RoomEvents.Join] = function(socket, packet) {
    try {
        packet.value = rooms[packet.value] == undefined || !(rooms[packet.value].tryJoin(socket));
        socket.send(packet.asPacket());
    } catch {
        packet.value = false;
        socket.send(packet.asPacket);
    }
}

roomHandler[enums.RoomEvents.Quit] = function(socket, packet) {
    packet.value = socket.room.tryQuit(socket, rooms);
    socket.send(packet.asPacket);
}

roomHandler[enums.RoomEvents.Remove] = function(socket, packet) {
    try {
        var quitPacket = new Packet(enums.Types.Room, enums.RoomEvents.Quit, '');
        socket.room.players.forEach(soc => {
            if(soc != socket)
                soc.send(quitPacket);
        });
    
        packet.value = true;
        socket.send(packet.asPacket());
    } catch {
        packet.value = false;
        socket.send(packet.asPacket());
    }
}

exports.roomHandler;