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
    Back2Lobby : 5,
    OtherQuit : 6,
};

exports.GameManagerEvents = {
    MatchMakingStart : 0,
    Ready : 1,
    Start : 2,
    SetStage : 3,
    Fight : 4,
    Finish : 5,
    MatchMakingStop : 6,
};

exports.InteractEvents = {
    Damage : 0,
    PlayerMove : 1,
    DragonMove : 2,
    Spawn : 3,
    BoolAnim : 4,
    Ride : 5,
    TriggerAnim : 6,
    Fire : 7,
    DragonDie : 8,
};

exports.ErrorEvents = {
    ErrorMessage : 0,
}