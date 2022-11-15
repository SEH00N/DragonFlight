const Enums = require('../Enums/Enums.js');
const Room = require('../Classes/Room.js').Room;
const Packet = require('../Classes/Packet.js').Packet;

const handler = [];
const startInterval = 3;

let matchMakingQueue = [];

handler[Enums.GameManagerEvents.MatchMakingStop] = function (socket, packet) {
    const index = matchMakingQueue.indexOf(socket);
    if(index > -1)
    {
        matchMakingQueue.splice(matchMakingQueue.indexOf(socket), 1);
        console.log('\x1b[33m%s\x1b[0m', `[GameManager] match making cancled`);
    }
}

handler[Enums.GameManagerEvents.MatchMakingStart] = function (socket, packet) {
    setTimeout(() => {
        console.log('\x1b[33m%s\x1b[0m', `[GameManager] match making started`);
        matchMakingQueue.push(socket);
        console.log(matchMakingQueue.length);

        if(matchMakingQueue.length >= 2)
            matchMaking();
    }, Math.random() * 10 * 1000)

}

const matchMaking = function() {
        var host = matchMakingQueue.randPop();
        var guest = matchMakingQueue.randPop();

        var room = new Room(host);
        host.roomId = room.id;

        if(room.tryJoin(guest))
            guest.roomId = room.id;
        else 
            throw new Error();
    
        global.rooms[room.id] = room;
    
        var packet = new Packet(Enums.Types.GameManager, Enums.GameManagerEvents.Start, "");
        room.players.forEach(soc => {
            soc.ready = false;
            packet.value = (host == soc) ? 0 : 1;
            soc.send(packet.asPacket());
        });
    
        console.log('\x1b[33m%s\x1b[0m', `[GameManager] match made`);
}

Array.prototype.randPop = function() {
    var randIndex = Math.floor(Math.random()* this.length);
    var element = this[randIndex];
    this.splice(randIndex, 1);
    return element;
}

handler[Enums.GameManagerEvents.Finish] = function(socket, packet) {
    console.log('\x1b[33m%s\x1b[0m', `[GameManager] game finished | room : ${socket.roomId}`);

    global.rooms[socket.roomId].players.forEach(soc => {
        if(soc == socket) {
            packet.value = false;
            soc.send(packet.asPacket());
        } else {
            packet.value = true;
            soc.send(packet.asPacket());
        }
    });
}

handler[Enums.GameManagerEvents.Ready] = function(socket, packet) {
    if (socket.ready == undefined)
        socket.ready = false;

    console.log('\x1b[33m%s\x1b[0m', `[GameManager] ready to fight | room : ${socket.roomId}`);

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

    console.log('\x1b[33m%s\x1b[0m', `[GameManager] joining game | room : ${socket.roomId}`);

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

    console.log('\x1b[33m%s\x1b[0m', `[GameManager] start fight | room : ${socket.roomId}`);

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