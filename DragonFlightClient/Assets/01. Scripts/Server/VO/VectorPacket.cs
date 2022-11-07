using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public struct VectorPacket
{
    [JsonProperty("x")] public float x;
    [JsonProperty("y")] public float y;
    [JsonProperty("z")] public float z;

    public VectorPacket(Vector3 vector)
    {
        this.x = vector.x;
        this.y = vector.y;
        this.z = vector.z;
    }

    public static implicit operator VectorPacket(Vector3 vec) => new VectorPacket(vec);
    public static implicit operator Vector3(VectorPacket vec) => new Vector3(vec.x, vec.y, vec.z);
}