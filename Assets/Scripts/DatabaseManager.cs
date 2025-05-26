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

    public async Task Save(PlayerProfile profile)
    {
        switch (mode)
        {
            case SavingModes.Local:
                localSaveManager.Save(profile);
                break;
            
            case SavingModes.SQL:
                break;

            case SavingModes.Cloud:
                Debug.Log("Using Cloud Save");
                await cloudSaveManager.SavePlayerData(profile);
                break;

            case SavingModes.Firebase:
                await firebaseSaveManager.SavePlayerData(profile);
                break;

            default:
                break;
        }
    }

    public async Task Load()
    {
        switch (mode)
        {
            case SavingModes.Local:
                loadedProfile =  localSaveManager.Load();
                break;

            case SavingModes.SQL:
                break;

            case SavingModes.Cloud:
                Debug.Log("Using Cloud Load");

                await cloudSaveManager.LoadPlayerData();
                loadedProfile = cloudSaveManager.tempLoad;
                break;

            case SavingModes.Firebase:
                await firebaseSaveManager.LoadPlayerData();
                loadedProfile = firebaseSaveManager.tempLoad;
                break;

            default:
                break;
        }

    }
}
