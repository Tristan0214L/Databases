using System.IO;
using UnityEngine;

public class LocalSaveManager : MonoBehaviour
{
    //for now we can save locally
    private string path = "";

    private void Awake()
    {
        path = Path.Combine(Application.persistentDataPath, "playerData.json");
    }

    public void Save(PlayerProfile profile)
    {
        string json = JsonUtility.ToJson(profile, true);
        File.WriteAllText(path, json);
        Debug.Log("Saved to: " + path);
    }

    public PlayerProfile Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<PlayerProfile>(json);

        }
        return null;
    }
}
