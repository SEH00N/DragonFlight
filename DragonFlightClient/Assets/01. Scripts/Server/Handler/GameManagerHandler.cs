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

        handlers[(int)GameManagerEvents.Ready] = ReadyEvent;
        handlers[(int)GameManagerEvents.Start] = StartEvent;
        handlers[(int)GameManagerEvents.SetStage] = SetStageEvent;
        handlers[(int)GameManagerEvents.Fight] = FightEvent;
        handlers[(int)GameManagerEvents.Finish] = FinishEvent;
    }

    private void FinishEvent(Packet packet)
    {
        if(!bool.TryParse(packet.value, out bool win))
            return;

        if(DEFINE.Dragon != null)
            DEFINE.Dragon.OnFinish();
        if(DEFINE.Player != null)
            DEFINE.Player.OnFinish();
        if(DEFINE.OtherDragon != null)
            DEFINE.OtherDragon.DoDissolve();
        if(DEFINE.OtherPlayer != null)
            DEFINE.OtherPlayer.DoDissolve();

        DEFINE.Ready2Start = false;
        DEFINE.GameOver = true;

        DEFINE.MainCanvas.Find("GameOverPanel").GetComponent<GameOverPanel>().GameOver(win);
        GameManager.Instance.CursorActive = true;
    }

    private void FightEvent(Packet packet)
    {
        DEFINE.MainCanvas.Find("BlockPanel").gameObject.SetActive(false);
        DEFINE.Ready2Start = true;
        DEFINE.Player.PlayerMovement.Active = true;
        DEFINE.Player.WeaponHandler.Active = true;

        GameManager.Instance.CursorActive = false;
    }

    private void SetStageEvent(Packet packet)
    {
        // 보류
    }

    private void StartEvent(Packet packet)
    {
        if(int.TryParse(packet.value, out int order))
            SceneLoader.Instance.LoadAsync("InGame", () => {
                GameManager.Instance.SetGame(order); 

                AudioManager.Instance.PlayBGM("InGameBGM");
            });
    }

    private void ReadyEvent(Packet packet)
    {
        Transform readyTextObject = DEFINE.MainCanvas.Find("OtherInfo/ReadyButton/ReadyText");
        if(readyTextObject == null)
            return;

        readyTextObject.GetComponent<TextMeshProUGUI>().text = packet.value == "true" ? "READY!!" : ""; 
    }
}
