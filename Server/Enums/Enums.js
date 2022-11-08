exports.Types = {
    Room : 0,
    GameManager : 1,
    Interact : 2,
    Chat : 3,
    Error : 4,
    ETC : 5,
};

exports.RoomEvents = {
    Create : 0,
    Join : 1,
    Quit : 2,
    Remove : 3,
    OtherJoin : 4,
};

exports.GameManagerEvents = {
    MatchMaking : 0,
    Ready : 1,
    Start : 2,
    SetStage : 3,
    Fight : 4,
};

exports.InteractEvents = {
    Damage : 0,
    PlayerMove : 1,
    DragonMove : 2,
    Spawn : 3,
    BoolAnim : 4,
    Ride : 5,
};