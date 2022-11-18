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
        
        if(instance != null)
            return;

        if (!Directory.Exists(saveFolderPath)) Directory.CreateDirectory(saveFolderPath);

        if(!TryReadJson<UserSetting>(out userSetting))
            userSetting = new UserSetting().Generate();
    }

    // private void OnApplicationQuit()
    // {
    //     SaveData<UserSetting>(userSetting);
    // }

    private void OnDestroy()
    {
        SaveData<UserSetting>(userSetting);
    }

    private bool TryReadJson<T>(out T data) where T : Data
    {
        string json = File.ReadAllText(GetPath<T>());

        if (json.Length > 0)
        {
            data = JsonConvert.DeserializeObject<T>(json);

            if(data.IsNull())
            {
                data = default(T);
                return false;
            }

            return true;
        }
        else
        {
            data = default(T);
            return false;
        }
    }

    private void SaveData<T>(T data) where T : Data
    {
        data.Save();

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