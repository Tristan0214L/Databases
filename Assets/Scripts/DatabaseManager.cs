using System.IO;
using System.Threading.Tasks;
using UnityEngine;


public enum SavingModes
{
    Local,
    SQL,
    Cloud,
    Firebase
}

public class DatabaseManager : MonoBehaviour
{
    //for now we can save locally
    public LocalSaveManager localSaveManager;
    public CloudSaveManager cloudSaveManager;
    public FirebaseSaveManager firebaseSaveManager;


    public SavingModes mode = SavingModes.Local;

    public PlayerProfile loadedProfile;

    public void Save(PlayerProfile profile)
    {
        switch (mode)
        {
            case SavingModes.Local:
                localSaveManager.Save(profile);
                break;
            
            case SavingModes.SQL:
                break;

            case SavingModes.Cloud:
                cloudSaveManager.SavePlayerData(profile);
                break;

            case SavingModes.Firebase:
                firebaseSaveManager.SavePlayerData(profile);
                break;

            default:
                break;
        }
    }

    public void Load()
    {
        switch (mode)
        {
            case SavingModes.Local:
                loadedProfile =  localSaveManager.Load();
                break;

            case SavingModes.SQL:
                break;

            case SavingModes.Cloud:
                cloudSaveManager.LoadPlayerData();
                loadedProfile = cloudSaveManager.tempLoad;
                break;

            case SavingModes.Firebase:
                firebaseSaveManager.LoadPlayerData();
                loadedProfile = firebaseSaveManager.tempLoad;
                break;

            default:
                break;
        }

    }
}
