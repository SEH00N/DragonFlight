using Newtonsoft.Json;
using UnityEngine;

public struct MovePacket
{
    [JsonProperty("p")] public Vector3 position;
    [JsonProperty("r")] public Quaternion rotation;
    [JsonProperty("v")] public float animationValue; 

    public MovePacket(Vector3 position, Quaternion rotation, float animationValue)
    {
        this.position = position;
        this.rotation = rotation;
        this.animationValue = animationValue;
    }
}
