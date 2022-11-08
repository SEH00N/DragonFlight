using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void DoStart()
    {
        Client.Instance.SendMessages((int)Types.GameManagerEvent, (int)GameManagerEvents.Start, "");
    }
}
