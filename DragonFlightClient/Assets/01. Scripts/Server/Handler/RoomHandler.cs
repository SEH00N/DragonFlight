using TMPro;
using UnityEngine.SceneManagement;

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
        DEFINE.MainCanvas.Find("OtherPlayer").GetComponent<PlayerInfo>().Init("");
    }

    private void JoinEvent(Packet packet)
    {
        if(bool.TryParse(packet.value, out bool success) && success)
        {
            SceneLoader.Instance.LoadAsync("Lobby", () => {
                DEFINE.MainCanvas.Find("MyInfo").GetComponent<PlayerInfo>().Init("READY?");
                Client.Instance.SendMessages((int)Types.RoomEvent, (int)RoomEvents.OtherJoin, "");
            });
        }
    }

    private void CreateEvent(Packet packet)
    {
        if(bool.TryParse(packet.value, out bool success) && success)
        {
            SceneLoader.Instance.LoadAsync("Lobby", () => {
                DEFINE.MainCanvas.Find("MyInfo").GetComponent<PlayerInfo>().Init("READY?");
                DEFINE.MainCanvas.Find("CodeInfo/RoomCode").GetComponent<TextMeshProUGUI>().text = "";
         });
        }
    }
}
