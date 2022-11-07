using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomHandler : Handler
{
    public override int HandlersSize => (int)RoomEvents.Last;
    
    public override void CreateHandler()
    {
        base.CreateHandler();

        handlers[(int)RoomEvents.Create] = CreateEvent;
        handlers[(int)RoomEvents.Join] = JoinEvent;
    }

    private void JoinEvent(Packet packet)
    {
        if(bool.TryParse(packet.value, out bool success) && success)
        {
            SceneManager.LoadScene("WaitingRoom");
        }
    }

    private void CreateEvent(Packet packet)
    {
        if(bool.TryParse(packet.value, out bool success) && success)
        {
            SceneManager.LoadScene("WaitingRoom");
        }
    }
}
