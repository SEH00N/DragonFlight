using Newtonsoft.Json;

public struct DamagePacket
{
    [JsonProperty("t")] public string target;
    [JsonProperty("d")] public float damage;

    public DamagePacket(string target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }
}
