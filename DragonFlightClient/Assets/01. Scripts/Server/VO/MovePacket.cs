using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public struct MovePacket
{
    [JsonProperty("p")] public VectorPacket position;
    [JsonProperty("r")] public VectorPacket rotation;
    [JsonProperty("v")] public float animationValue; 

    public MovePacket(Vector3 position, Vector3 rotation, float animationValue)
    {
        this.position = position;
        this.rotation = rotation;
        this.animationValue = animationValue;
    }
}
