using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToLobby : MonoBehaviour
{
    public void DoBackToLobby()
    {
        Client.Instance.SendMessages((int)Types.RoomEvent, (int)RoomEvents.Back2Lobby, " ");
    }
}
