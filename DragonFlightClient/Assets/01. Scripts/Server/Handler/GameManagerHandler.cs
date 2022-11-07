using Newtonsoft.Json;
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
    }

    private void SetStageEvent(Packet packet)
    {
        
    }

    private void StartEvent(Packet packet)
    {
        
    }

    private void ReadyEvent(Packet packet)
    {
        
    }

    private void MatchMakingEvent(Packet packet)
    {
        
    }
}
