using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitRoom : MonoBehaviour
{
    public void DoQuit()
    {
        Client.Instance.SendMessages((int)Types.RoomEvent, (int)RoomEvents.Quit, "");
    }
}
