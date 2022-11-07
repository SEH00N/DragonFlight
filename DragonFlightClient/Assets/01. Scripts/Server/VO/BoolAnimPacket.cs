using Newtonsoft.Json;
using UnityEngine;

public struct BoolAnimPacket
{
    [JsonProperty("t")] public string target;
    [JsonProperty("k")] public string key;
    [JsonProperty("v")] public bool value;
    
    public BoolAnimPacket(string target, string key, bool value)
    {
        this.target = target;
        this.key = key;
        this.value = value;
    }
}
