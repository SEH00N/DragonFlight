const Enums = require('../Enums/Enums.js');
const Room = require('../Classes/Room.js').Room;
const Packet = require('../Classes/Packet.js').Packet;

const handler = [];
global.rooms = {};

handler[Enums.RoomEvents.Back2Lobby] = function(socket, packet) {
    packet.event = Enums.RoomEvents.Join;
    packet.value = JSON.stringify({
        c : socket.roomId,
        r : false,
        s : (global.rooms[socket.roomId] != undefined)
    });

    console.log('\x1b[33m%s\x1b[0m', `[RoomSystem] back to lobby | code : ${socket.roomId}`);

    socket.send(packet.asPacket());
}

handler[Enums.RoomEvents.Create] = function(socket, packet) {
    try {
        var room = new Room(socket);
        global.rooms[room.id] = room;
        socket.roomId = room.id;
        
        packet.value = JSON.stringify({
            c : room.id,
            s : true,
        });

        console.log('\x1b[33m%s\x1b[0m', `[RoomSystem] room created | code : ${room.id}`);
        socket.send(packet.asPacket());
    } catch {
        packet.value = JSON.stringify({
            c : room.id,
            s : false,
        });

        console.log('\x1b[33m%s\x1b[0m', `[RoomSystem] failed to creat room | code : ${room.id}`);
        socket.send(packet.asPacket());
    }
}

handler[Enums.RoomEvents.Join] = function(socket, packet) {
    try {
        id = packet.value;

        if(global.rooms[id] != undefined && global.rooms[id].tryJoin(socket))
        {
            socket.roomId = id;

            packet.value = JSON.stringify({
                c: id,
                r: global.rooms[id].host.ready,
                s: true,
            });
        }
        else
            packet.value = JSON.stringify({
                c: id,
                r: false,
                s: false,
            });

        console.log('\x1b[33m%s\x1b[0m', `[RoomSystem] client joined room | code : ${packet.value.c}`);
        socket.send(packet.asPacket());
    } catch {
        packet.value = JSON.stringify({
            c : id,
            s : false,
        });

        console.log('\x1b[33m%s\x1b[0m', `[RoomSystem] client failed to join room | code : ${packet.value.c}`);
        socket.send(packet.asPacket());
    }
}

handler[Enums.RoomEvents.Quit] = function(socket, packet) {
    room = global.rooms[soclet.roomId];

    packet.value = room.tryQuit(socket);
    
    console.log('\x1b[33m%s\x1b[0m', `[RoomSystem] client ${packet.value ? 'succeed' : 'failed'} to quit room | code : ${socket.roomId}`);

    socket.send(packet.asPacket());

    console.log(room.players.length);

    if(packet.value) {
        var otherQuitPacket = new Packet(Enums.Types.Room, Enums.RoomEvents.OtherQuit, '');
        room.host.send(otherQuitPacket.asPacket());
        
        socket.roomId = undefined;
    }
}

handler[Enums.RoomEvents.Remove] = function(socket, packet) {
    try {
        var quitPacket = new Packet(Enums.Types.Room, Enums.RoomEvents.Quit, '');
        global.rooms[socket.roomId].players.forEach(soc => {
            if(soc != socket)
                soc.send(quitPacket.asPacket());
        });
        
        global.rooms[socket.roomId] = undefined;
        
        packet.value = true;

        console.log('\x1b[33m%s\x1b[0m', `[RoomSystem] room removed | code : ${socket.roomId}`);

        socket.roomId = undefined;

        socket.send(packet.asPacket());
    } catch {
        packet.value = false;

        console.log('\x1b[33m%s\x1b[0m', `[RoomSystem] failed to remove room | code : ${socket.roomId}`);
        socket.send(packet.asPacket());
    }
}

handler[Enums.RoomEvents.OtherJoin] = function(socket, packet) {
    global.rooms[socket.roomId].host.send(packet.asPacket());
}

exports.handler = handler;