using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchMakingReq : MonoBehaviour
{
    public void DoMatchMakingStart()
    {
        Client.Instance.SendMessages((int)Types.GameManagerEvent, (int)GameManagerEvents.MatchMakingStart, "");
    }

    public void DoMatchmakingStop()
    {
        Client.Instance.SendMessages((int)Types.GameManagerEvent, (int)GameManagerEvents.MatchMakingStop, "");
    }
}
