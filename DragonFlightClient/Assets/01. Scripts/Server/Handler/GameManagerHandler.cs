using System;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class GameManagerHandler : Handler
{
    public override int HandlersSize => (int)GameManagerEvents.Last;

    public override void CreateHandler()
    {
        base.CreateHandler();

        handlers[(int)GameManagerEvents.MatchMaking] = MatchMakingEvent;
        handlers[(int)GameManagerEvents.Ready] = ReadyEvent;
        handlers[(int)GameManagerEvents.Start] = StartEvent;
        handlers[(int)GameManagerEvents.SetStage] = SetStageEvent;
        handlers[(int)GameManagerEvents.Fight] = FightEvent;
    }

    private void FightEvent(Packet packet)
    {
        DEFINE.MainCanvas.Find("BlockPanel").gameObject.SetActive(false);
        DEFINE.Ready2Start = true;
        DEFINE.Player.PlayerMovement.Active = true;
    }

    private void SetStageEvent(Packet packet)
    {
        // 보류
    }

    private void StartEvent(Packet packet)
    {
        if(int.TryParse(packet.value, out int order))
            SceneLoader.Instance.LoadAsync("InGame", () => GameManager.Instance.SetGame(order) );
    }

    private void ReadyEvent(Packet packet)
    {
        DEFINE.MainCanvas.Find("OtherInfo/ReadyButton/ReadyText").GetComponent<TextMeshProUGUI>().text = packet.value == "true" ? "READY!!" : ""; 
    }

    private void MatchMakingEvent(Packet packet)
    {
        // 보류
    }
}
