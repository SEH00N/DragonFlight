using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JoinRoom : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField = null;

    public void DoJoin()
    {
        Client.Instance.SendMessages((int)Types.RoomEvent, (int)RoomEvents.Join, inputField.text);
    }
}
