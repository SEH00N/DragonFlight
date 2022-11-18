using UnityEngine;

public class SurrenderGame : MonoBehaviour
{
    public void DoSurrender()
    {
        Client.Instance.SendMessages((int)Types.GameManagerEvent, (int)GameManagerEvents.Finish, "");
    }
}
