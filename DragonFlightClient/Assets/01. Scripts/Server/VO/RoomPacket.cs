using Newtonsoft.Json;

public struct RoomPacket
{
    [JsonProperty("c")] public string code;
    [JsonProperty("r")] public bool otherReady;
    [JsonProperty("s")] public bool success;

    public RoomPacket(string code, bool otherReady, bool success)
    {
        this.code = code;
        this.otherReady = otherReady;
        this.success = success;
    }
}
