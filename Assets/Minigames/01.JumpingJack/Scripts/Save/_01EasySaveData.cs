using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public int intValue;
}

public class _01EasySaveData : MonoBehaviour
{
    [SerializeField] private SaveData data;

    private string savePath;
    public static _01EasySaveData Instance;
    private void Awake()
    {
        Instance = this;
        savePath = Application.persistentDataPath + "/save.txt";
        Debug.LogWarning($"  SAVED IN :  {savePath}");
        Load();
    }

    public void Save()
    {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, jsonData);
    }

    public int Load()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            data = JsonUtility.FromJson<SaveData>(jsonData);
        }
        else
        {
            data = new SaveData();
        }
        return data.intValue;
    }

    public void UnlockLevels(int value = 0)
    {
        data.intValue = value;
        Save();
    }
}
