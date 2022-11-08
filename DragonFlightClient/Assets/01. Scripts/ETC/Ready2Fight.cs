using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ready2Fight : MonoBehaviour
{
    private void Start()
    {
        Client.Instance.SendMessages((int)Types.GameManagerEvent, (int)GameManagerEvents.Fight, "");
    }
}
