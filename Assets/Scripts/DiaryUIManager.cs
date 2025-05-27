using System.Collections.Generic;
using TMPro;
using Unity.Services.CloudSave.Models.Data.Player;
using UnityEngine;
using UnityEngine.UI;

public class DiaryUIManager : MonoBehaviour
{
    public List<DiaryEntry> diaryEntries = new List<DiaryEntry>();

    //INPUTTTT
    public TMP_InputField title;
    public TMP_InputField description;
    public TMP_Dropdown mood;

    

    public Button saveDiary;

    //scene object
    public GameObject mainList;

    //prefab
    public GameObject diaryEntryPrefab;

    public void AddDiaryEntry()
    {
        //input is entered

        //variable is created
        DiaryEntry diaryEntry = new DiaryEntry()
        {
            title = this.title.text,
            description = this.description.text,
            mood = (Moods)this.mood.value

        };


        diaryEntries.Add(diaryEntry);

        var go = Instantiate(this.diaryEntryPrefab, mainList.transform);
        go.GetComponentInChildren<TMP_Text>().text = diaryEntry.title;

        //added to list (visually and programmatically)
    }

}
