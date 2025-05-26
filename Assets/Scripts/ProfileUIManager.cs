using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUIManager : MonoBehaviour
{
    [SerializeField]
    PlayerProfile playerProfile;

    [SerializeField]
    DatabaseManager databaseManager;



    public TMPro.TMP_InputField nameInput;
    public TMP_Dropdown colourDropdown;

    
    public Button saveButton;
    public Image colourDisplay;

    private Color[] availableColours = new Color[] {
        Color.gray,
        Color.white,
        Color.yellow,
        Color.red,
        Color.green,
        Color.blue,
        Color.magenta,
        Color.clear
    };



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public async Task Start()
    {
        await Task.Delay(1000);
        //On startup create our dropdown
        colourDropdown.ClearOptions();
        List<string> options = new List<string>();

        foreach (var colour in availableColours) {
            options.Add(ColorUtility.ToHtmlStringRGB(colour));

        }

        colourDropdown.AddOptions(options);

        //if there is data please load
        await LoadData();
        

        if (playerProfile != null)
        {
            nameInput.text = playerProfile.playerName;
            var color = playerProfile.GetColour();
            colourDisplay.color = color;

            int index = System.Array.FindIndex(availableColours, c => c == color);
            if(index >= 0) colourDropdown.value = index;
        }

        colourDropdown.onValueChanged.AddListener(OnColourChanged);
        
        saveButton.onClick.AddListener(CallSave);




    }
    void OnColourChanged(int index)
    {
        colourDisplay.color = availableColours[index];
    }
    async Task LoadData()
    {
        await databaseManager.Load();

        playerProfile = databaseManager.loadedProfile;
       
    }
    void CallSave()
    {
        SaveData();
    }
    async Task SaveData()
    {
        

        var data = new PlayerProfile(
            nameInput.text,
            availableColours[colourDropdown.value]
            );

        playerProfile = data;
        await databaseManager.Save( playerProfile );
        Debug.Log("Saved");
        
    }
}
