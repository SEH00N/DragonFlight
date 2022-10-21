exports.Types = {
    Room : 0,
    GameManager : 1,
    Interact : 2,
    Chat : 3,
    Error : 4,
    ETC : 5,
}

exports.RoomEvents = {
    Create : 0,
    Join : 1,
    Quit : 2,
    Remove : 3,
}

exports.GameManagerEvents = {
    MatchMaking : 0,
    Ready : 1,
    SetStage : 2,
}

exports.Interact = {
    Move : 0,
    Rotate : 1,
    Spawn : 3,
    
}