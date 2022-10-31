using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance = null;

    private string saveFolderPath = "./Save";

    private UserSetting ud = null;

    private void Awake()
    {
        saveFolderPath = Path.Combine(Application.dataPath, saveFolderPath);
        if (!Directory.Exists(saveFolderPath)) Directory.CreateDirectory(saveFolderPath);

        if(!TryReadJson<UserSetting>(out ud))
            ud = new UserSetting();
    }

    private void OnDisable()
    {
        SaveData<UserSetting>(ud);
    }

    private bool TryReadJson<T>(out T data)
    {
        string json = File.ReadAllText(GetPath<T>());

        if (json.Length > 0)
        {
            data = JsonConvert.DeserializeObject<T>(json);
            return true;
        }
        else
        {
            data = default(T);
            return false;
        }
    }

    private void SaveData<T>(T data)
    {
        string json = JsonConvert.SerializeObject(data);

        File.WriteAllText(GetPath<T>(), json);
    }

    private string GetPath<T>()
    {
        string path = $"{saveFolderPath}/{typeof(T)}.json";

        if(!File.Exists(path)) File.Create(path).Close();

        return path;
    }
}