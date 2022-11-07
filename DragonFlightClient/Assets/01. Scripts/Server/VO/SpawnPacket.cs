using Newtonsoft.Json;
using UnityEngine;

public struct SpawnPacket
{
    [JsonProperty("n")] public string name;
    [JsonProperty("p")] public VectorPacket position;
    [JsonProperty("r")] public VectorPacket rotation;

    public SpawnPacket(string name, Vector3 position, Vector3 rotation)
    {
        this.name = name;
        this.position = position;
        this.rotation = rotation;
    }
}
