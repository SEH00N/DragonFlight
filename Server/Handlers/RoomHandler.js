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

    console.log('\x1b[33m%s\x1b[0m', `[RoomManager] back to lobby | room : ${socket.roomId}`);

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

        console.log('\x1b[33m%s\x1b[0m', `[RoomManager] room created | room : ${room.id}`);
        socket.send(packet.asPacket());
    } catch {
        var errPacket = new Packet(Enums.Types.Error, Enums.ErrorEvents.ErrorMessage, "생성에 실패하였습니다.");

        console.log('\x1b[33m%s\x1b[0m', `[RoomManager] failed to creat room | room : ${room.id}`);
        socket.send(errPacket.asPacket());
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
            throw new Error();

        console.log('\x1b[33m%s\x1b[0m', `[RoomManager] client joined room | room : ${id}`);
        socket.send(packet.asPacket());
    } catch {
        var errPacket = new Packet(Enums.Types.Error, Enums.ErrorEvents.ErrorMessage, "방 참가에 실패하였습니다.");

        console.log('\x1b[33m%s\x1b[0m', `[RoomManager] client failed to join room | room : ${id}`);
        socket.send(errPacket.asPacket());
    }
}

handler[Enums.RoomEvents.Quit] = function(socket, packet) {
    var room = global.rooms[socket.roomId];
    socket.ready = false;

    packet.value = room.tryQuit(socket);
    
    console.log('\x1b[33m%s\x1b[0m', `[RoomManager] client ${packet.value ? 'succeed' : 'failed'} to quit room | room : ${socket.roomId}`);

    socket.send(packet.asPacket());

    if(packet.value) {
        var otherQuitPacket = new Packet(Enums.Types.Room, Enums.RoomEvents.OtherQuit, '');
        room.host.send(otherQuitPacket.asPacket());
        
        socket.roomId = undefined;
    }
}

handler[Enums.RoomEvents.Remove] = function(socket, packet) {
    try {
        var quitPacket = new Packet(Enums.Types.Room, Enums.RoomEvents.Quit, true);
        global.rooms[socket.roomId].players.forEach(soc => {
            if(soc != socket)
                soc.send(quitPacket.asPacket());
        });
        
        global.rooms[socket.roomId] = undefined;
        
        packet.value = true;

        console.log('\x1b[33m%s\x1b[0m', `[RoomManager] room removed | room : ${socket.roomId}`);

        socket.roomId = undefined;

        socket.send(packet.asPacket());
    } catch {
        var errPacket = new Packet(Enums.Types.Error, Enums.ErrorEvents.ErrorMessage, "방 제거에 실패하였습니다.");

        console.log('\x1b[33m%s\x1b[0m', `[RoomManager] failed to remove room | room : ${socket.roomId}`);
        socket.send(errPacket.asPacket());
    }
}

handler[Enums.RoomEvents.OtherJoin] = function(socket, packet) {
    global.rooms[socket.roomId].host.send(packet.asPacket());
}

exports.handler = handler;