using System.Collections.Generic;
using Newtonsoft.Json;

public class UserSetting : Data
{
    [JsonProperty("volumes")] public Dictionary<string, float> volumes;
    [JsonProperty("mouseSensitivity")] public float mouseSensitivity = 4f;

    public UserSetting Generate()
    {
        volumes = new Dictionary<string, float>() {
            ["masterVolume"] = 0f,
            ["sfxVolume"] = 0f,
            ["bgmVolume"] = 0f,
        };

        mouseSensitivity = 4f;
        
        return this;
    }

    public override bool IsNull()
    {
        if(volumes == null)
            return true;

        return false;
    }

    public override void Save()
    {
        mouseSensitivity = DEFINE.MouseSensitivity;
    }
}
