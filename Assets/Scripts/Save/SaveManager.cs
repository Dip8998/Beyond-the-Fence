using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string Path =>
        Application.persistentDataPath + "/save.json";

    public static void Save(SaveData data)
    {
        File.WriteAllText(Path, JsonUtility.ToJson(data, true));
        Debug.Log("[SAVE] Saved");
    }

    public static SaveData Load()
    {
        if (!File.Exists(Path))
            return null;

        Debug.Log("[SAVE] Loaded");
        return JsonUtility.FromJson<SaveData>(
            File.ReadAllText(Path)
        );
    }

    public static bool HasSave() => File.Exists(Path);

    public static void DeleteSave()
    {
        if (File.Exists(Path))
            File.Delete(Path);
    }
}