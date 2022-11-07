const Enums = require('../Enums/Enums.js');
const handler = [];

handler[Enums.InteractEvents.PlayerMove] = (socket, data) => broadCast(socket, data, false);
handler[Enums.InteractEvents.DragonMove] = (socket, data) => broadCast(socket, data, false);

const broadCast = (socket, data, toAll = false) => {
    global.rooms[socket.roomId].players.forEach(soc => {
        if (toAll == false && soc == socket)
            return;
        soc.send(data.asPacket());
    });
}

exports.handler = handler;