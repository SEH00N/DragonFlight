using System.Collections.Generic;
using Newtonsoft.Json;

public struct UserSetting
{
    [JsonProperty("volumes")] public Dictionary<string, float> volumes;

    public UserSetting Generate()
    {
        volumes = new Dictionary<string, float>() {
            ["masterVolume"] = 0f,
            ["sfxVolume"] = 0f,
            ["bgmVolume"] = 0f,
        };
        
        return this;
    }
}
