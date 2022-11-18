using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoom : MonoBehaviour
{
    public void DoCreate()
    {
        Client.Instance.SendMessages((int)Types.RoomEvent, (int)RoomEvents.Create, "");
    }
}
