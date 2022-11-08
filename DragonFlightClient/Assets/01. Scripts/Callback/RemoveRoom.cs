using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRoom : MonoBehaviour
{
    public void DoRemove()
    {
        Client.Instance.SendMessages((int)Types.RoomEvent, (int)RoomEvents.Remove, "");
    }
}
