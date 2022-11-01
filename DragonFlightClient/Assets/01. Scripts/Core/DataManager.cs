using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance = null;
    public static DataManager Instance {
        get {
            if(instance == null)
                instance = FindObjectOfType<DataManager>();

            return instance;
        }
    }

    private string saveFolderPath = "./Save";

    public UserSetting userSetting;

    private void Awake()
    {
        saveFolderPath = Path.Combine(Application.dataPath, saveFolderPath);
        if (!Directory.Exists(saveFolderPath)) Directory.CreateDirectory(saveFolderPath);

        if(!TryReadJson<UserSetting>(out userSetting))
            userSetting = new UserSetting().Generate();
    }

    private void OnDisable()
    {
        SaveData<UserSetting>(userSetting);
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