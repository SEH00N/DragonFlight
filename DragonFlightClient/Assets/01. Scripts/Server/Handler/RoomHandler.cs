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
        handlers[(int)RoomEvents.OtherQuit] = OtherQuitEvent;
    }

    private void OtherQuitEvent(Packet packet)
    {
        Transform otherInfoObject = DEFINE.MainCanvas.Find("OtherInfo");
        if(otherInfoObject == null)
            return;
            
        otherInfoObject.GetComponent<PlayerInfo>().Quit();

        TextPrefab txt = PoolManager.Instance.Pop("NoticeTextPrefab") as TextPrefab;
        txt.Init("상대방이 퇴장하였습니다.", DEFINE.StaticCanvas);
        txt.DoNoticeTrail();
    }

    private void QuitEvent(Packet packet)
    {
        if(bool.TryParse(packet.value, out bool success) && success)
            SceneLoader.Instance.LoadAsync("MainMenu", () => {
                TextPrefab txt =  PoolManager.Instance.Pop("NoticeTextPrefab") as TextPrefab;
                txt.Init("퇴장되었습니다.", DEFINE.StaticCanvas);
                txt.DoNoticeTrail();

                AudioManager.Instance.PlayBGM("IntroBGM");
            }); 
        else {
            TextPrefab txt =  PoolManager.Instance.Pop("NoticeTextPrefab") as TextPrefab;
            txt.Init("퇴장에 실패하였습니다.", DEFINE.StaticCanvas);
            txt.DoNoticeTrail();
        }
    }

    private void OtherJoinEvent(Packet packet)
    {
        Transform otherInfo = DEFINE.MainCanvas.Find("OtherInfo");
        if(otherInfo != null)
            otherInfo.GetComponent<PlayerInfo>().Init("");
    }

    private void JoinEvent(Packet packet)
    {
        RoomPacket roomPacket = JsonConvert.DeserializeObject<RoomPacket>(packet.value);

        if(roomPacket.success)
            SceneLoader.Instance.LoadAsync("Lobby", () => {
                SetUI(false, roomPacket.otherReady, roomPacket.code);
                Client.Instance.SendMessages((int)Types.RoomEvent, (int)RoomEvents.OtherJoin, "");
                
                AudioManager.Instance.PlayBGM("LobbyBGM");
            });
    }

    private void CreateEvent(Packet packet)
    {
        RoomPacket roomPacket = JsonConvert.DeserializeObject<RoomPacket>(packet.value);

        if(roomPacket.success)
            SceneLoader.Instance.LoadAsync("Lobby", () => {
                SetUI(true, false, roomPacket.code);
                GUIUtility.systemCopyBuffer = roomPacket.code;

                AudioManager.Instance.PlayBGM("LobbyBGM");
            });
    }

    private void SetUI(bool isHost, bool otherReady, string code)
    {
        Transform mainCanvas = DEFINE.MainCanvas;

        mainCanvas.Find("MyInfo").GetComponent<PlayerInfo>().Init("READY?");
        mainCanvas.Find("OtherInfo").GetComponent<PlayerInfo>().Init(otherReady ? "READY!!" : "");
        mainCanvas.Find("CodeInfo/RoomCode").GetComponent<TextMeshProUGUI>().text = code;
        mainCanvas.Find(isHost ? "QuitButton" : "RemoveButton").gameObject.SetActive(false);
    }
}
