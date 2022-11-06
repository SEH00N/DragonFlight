using UnityEngine;

public class RoomHandler : Handler
{
    public override int HandlersSize => (int)RoomEvents.Last;
    
    public override void CreateHandler()
    {
        base.CreateHandler();
    }
}
