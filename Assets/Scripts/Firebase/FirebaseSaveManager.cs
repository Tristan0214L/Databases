using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseSaveManager : MonoBehaviour
{
    public PlayerProfile tempLoad;

    private DatabaseReference dbRef;

    private async void Awake()
    {
        await InitializeFirebase();
    }

    public async Task InitializeFirebase()
    {
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

        if (dependencyStatus == DependencyStatus.Available)
        {
            dbRef = FirebaseDatabase.DefaultInstance.RootReference;
            Debug.Log("Firebase initialized successfully.");
        }
        else
        {
            Debug.LogError($"Firebase dependency error: {dependencyStatus}");
        }
    }

    public async Task SavePlayerData(PlayerProfile profile)
    {
        if (dbRef == null)
        {
            Debug.LogError("Firebase not initialized.");
            return;
        }

        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "playerName", profile.playerName },
            { "favouriteColor", profile.favouriteColourHex }
        };

        string userId = SystemInfo.deviceUniqueIdentifier;

        await dbRef.Child("players").Child(userId).SetValueAsync(data);
        Debug.Log("Player data saved to Firebase.");
    }

    public async Task LoadPlayerData()
    {
        if (dbRef == null)
        {
            Debug.LogError("Firebase not initialized.");
            return;
        }

        string userId = SystemInfo.deviceUniqueIdentifier;

        DataSnapshot snapshot = await dbRef.Child("players").Child(userId).GetValueAsync();

        if (snapshot.Exists)
        {
            string name = snapshot.Child("playerName")?.Value?.ToString() ?? "DefaultPlayer";
            string colourHex = snapshot.Child("favouriteColor")?.Value?.ToString() ?? "#ffffff";

            tempLoad = new PlayerProfile(name, colourHex);
            Debug.Log($"Loaded Player: {name}, Color: {colourHex}");
        }
        else
        {
            Debug.LogWarning("No player data found.");
        }
    }
}

