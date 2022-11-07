const enums = require('../Enums/Enums.js');

class Room {
    players = [];

    constructor(host) {
        this.create();
        this.host = host;
        this.players.push(host);
    }

    create() {
        do this.id = Math.random().toString(36).substring(2, 11).toUpperCase();
        while (global.rooms[this.id] != undefined)
    }

    tryJoin(socket) {
        if (this.players.length > 2)
            return false;
        else {
            this.players.push(socket);
            return true;
        }
    }

    tryQuit(socket) {
        try {
            this.players.filter(soc => soc != socket );
            return true;
        } catch {
            return false;
        }
    }
}

exports.Room = Room;