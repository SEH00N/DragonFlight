using UnityEngine;

public class RoomHandler : Handler
{
    public override int HandlersSize => (int)RoomEvents.Last;

    protected override void Awake()
    {
        base.Awake();

    }
}
