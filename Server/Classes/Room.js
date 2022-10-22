const enums = require('../Enums/Enums.js');

class Room {
    players = [];
    id;
    host;

    constructor(host, rooms) {
        this.create(rooms);
        this.host = host;
        this.players.push(host);
    }

    async create(rooms) {
        do this.id = Math.random().toString(36).substring(2, 11).toUpperCase();
        while (rooms[this.id] == undefined)
    }

    async tryJoin(socket) {
        if (this.players.length > 2)
            return false;
        else {
            this.players.push(socket);
            return true;
        }
    }

    async tryQuit(socket) {
        try {
            this.players.filter(soc => soc != socket );
            return true;
        } catch {
            return false;
        }
    }
}