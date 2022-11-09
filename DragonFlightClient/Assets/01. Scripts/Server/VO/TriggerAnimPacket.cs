using Newtonsoft.Json;
using UnityEngine;

public struct TriggerAnimPacket 
{
    [JsonProperty("t")] public string target;
    [JsonProperty("v")] public string value;
    
    public TriggerAnimPacket(string target, string value)
    {
        this.target = target;
        this.value = value;
    }
}
