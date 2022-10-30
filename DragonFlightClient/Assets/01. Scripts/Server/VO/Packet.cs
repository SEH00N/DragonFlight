using Newtonsoft.Json;

public struct Packet
{
    [JsonProperty("t")] public int type;
    [JsonProperty("e")] public int eventType;
    [JsonProperty("v")] public string value;

    public Packet(int type, int eventType, string value)
    {
        this.type = type;
        this.eventType = eventType;
        this.value = value;
    }
}
