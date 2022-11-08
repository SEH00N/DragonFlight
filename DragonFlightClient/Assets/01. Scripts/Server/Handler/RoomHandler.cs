using Newtonsoft.Json;
using TMPro;

public class RoomHandler : Handler
{
    public override int HandlersSize => (int)RoomEvents.Last;
    
    public override void CreateHandler()
    {
        base.CreateHandler();

        handlers[(int)RoomEvents.Create] = CreateEvent;
        handlers[(int)RoomEvents.Join] = JoinEvent;
        handlers[(int)RoomEvents.OtherJoin] = OtherJoinEvent;
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
            });
        }
    }
}
