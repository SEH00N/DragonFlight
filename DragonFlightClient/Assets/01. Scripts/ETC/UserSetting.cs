using Newtonsoft.Json;

public struct UserSetting
{
    [JsonProperty("sfxVolume")] public float sfxVolume;
    [JsonProperty("bgmVolume")] public float bgmVolume;
}
