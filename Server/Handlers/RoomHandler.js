const Enums = require('../Enums/Enums.js');
const Room = require('../Classes/Room.js').Room;
const Packet = require('../Classes/Packet.js').Packet;

const handler = [];
let rooms = {};

handler[Enums.RoomEvents.Create] = function(socket, packet) {
    try {
        var room = new Room(socket, rooms);
        console.log();
        rooms[room.id] = room;
        
        socket.room = room;
        
        packet.value = true;
        
        console.log('\x1b[33m%s\x1b[0m', `[RoomSystem] room created | code : ${room.id}`);
        socket.send(packet.asPacket());
    } catch {
        packet.value = false;

        console.log('\x1b[33m%s\x1b[0m', `[RoomSystem] failed to creat room | code : ${room.id}`);
        socket.send(packet.asPacket());
    }
}

handler[Enums.RoomEvents.Join] = function(socket, packet) {
    try {
        id = packet.value;

        if(rooms[id] != undefined && rooms[id].tryJoin(socket))
        {
            socket.room = rooms[id];
            packet.value = true;
        }
        else 
            packet.value = false;

        console.log('\x1b[33m%s\x1b[0m', `[RoomSystem] client joined room | code : ${packet.value}`);
        socket.send(packet.asPacket());
    } catch {
        packet.value = false;

        console.log('\x1b[33m%s\x1b[0m', `[RoomSystem] client failed to join room | code : ${packet.value}`);
        socket.send(packet.asPacket);
    }
}

handler[Enums.RoomEvents.Quit] = function(socket, packet) {
    packet.value = socket.room.tryQuit(socket, rooms);
    
    console.log('\x1b[33m%s\x1b[0m', `[RoomSystem] client ${packet.value ? 'succeed' : 'failed'} to quit room | code : ${socket.room.id}`);

    if(packet.value) socket.room = undefined;

    socket.send(packet.asPacket);
}

handler[Enums.RoomEvents.Remove] = function(socket, packet) {
    try {
        var quitPacket = new Packet(Enums.Types.Room, Enums.RoomEvents.Quit, '');
        socket.room.players.forEach(soc => {
            if(soc != socket)
                soc.send(quitPacket);
        });
        
        rooms[socket.room.id] = undefined;
        
        packet.value = true;

        console.log('\x1b[33m%s\x1b[0m', `[RoomSystem] room removed | code : ${socket.room.id}`);

        socket.room = undefined;

        socket.send(packet.asPacket());
    } catch {
        packet.value = false;

        console.log('\x1b[33m%s\x1b[0m', `[RoomSystem] failed to remove room | code : ${socket.room.id}`);
        socket.send(packet.asPacket());
    }
}

exports.handler = handler;