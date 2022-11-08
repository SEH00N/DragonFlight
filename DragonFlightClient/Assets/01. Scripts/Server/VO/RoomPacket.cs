using Newtonsoft.Json;

public struct RoomPacket
{
    [JsonProperty("c")] public string code;
    [JsonProperty("s")] public bool success;

    public RoomPacket(string code, bool success)
    {
        this.code = code;
        this.success = success;
    }
}
