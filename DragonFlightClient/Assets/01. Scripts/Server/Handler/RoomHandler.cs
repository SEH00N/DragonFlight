using System;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class RoomHandler : Handler
{
    public override int HandlersSize => (int)RoomEvents.Last;

    public override void CreateHandler()
    {
        base.CreateHandler();

        handlers[(int)RoomEvents.Create] = CreateEvent;
        handlers[(int)RoomEvents.Join] = JoinEvent;
        handlers[(int)RoomEvents.OtherJoin] = OtherJoinEvent;
        handlers[(int)RoomEvents.Quit] = QuitEvent;
        handlers[(int)RoomEvents.Remove] = QuitEvent;
    }

    private void QuitEvent(Packet packet)
    {
        SceneLoader.Instance.LoadAsync("MainMenu", () => {
            TextPrefab txt =  PoolManager.Instance.Pop("NoticeTextPrefab") as TextPrefab;
            txt.Init("퇴장되었습니다.");
        }); 
    }

    private void OtherJoinEvent(Packet packet)
    {
        DEFINE.MainCanvas.Find("OtherInfo").GetComponent<PlayerInfo>().Init("");
    }

    private void JoinEvent(Packet packet)
    {
        RoomPacket roomPacket = JsonConvert.DeserializeObject<RoomPacket>(packet.value);

        if(roomPacket.success)
        {
            SceneLoader.Instance.LoadAsync("Lobby", () => {
                DEFINE.MainCanvas.Find("MyInfo").GetComponent<PlayerInfo>().Init("READY?");
                DEFINE.MainCanvas.Find("OtherInfo").GetComponent<PlayerInfo>().Init("");
                DEFINE.MainCanvas.Find("CodeInfo/RoomCode").GetComponent<TextMeshProUGUI>().text = roomPacket.code;
                DEFINE.MainCanvas.Find("RemoveButton").gameObject.SetActive(false);
                Client.Instance.SendMessages((int)Types.RoomEvent, (int)RoomEvents.OtherJoin, "");
            });
        }
    }

    private void CreateEvent(Packet packet)
    {
        RoomPacket roomPacket = JsonConvert.DeserializeObject<RoomPacket>(packet.value);

        if(roomPacket.success)
        {
            SceneLoader.Instance.LoadAsync("Lobby", () => {
                DEFINE.MainCanvas.Find("MyInfo").GetComponent<PlayerInfo>().Init("READY?");
                DEFINE.MainCanvas.Find("CodeInfo/RoomCode").GetComponent<TextMeshProUGUI>().text = roomPacket.code;
                DEFINE.MainCanvas.Find("QuitButton").gameObject.SetActive(false);
                GUIUtility.systemCopyBuffer = roomPacket.code;
            });
        }
    }
}
