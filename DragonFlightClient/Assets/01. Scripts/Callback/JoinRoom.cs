using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JoinRoom : MonoBehaviour
{
    private TMP_InputField inputField = null;

    private void Awake()
    {
        inputField = DEFINE.MainCanvas.Find("GamePanel/InputField").GetComponent<TMP_InputField>();
    }

    public void DoJoin()
    {
        Client.Instance.SendMessages((int)Types.RoomEvent, (int)RoomEvents.Join, inputField.text);
    }
}
