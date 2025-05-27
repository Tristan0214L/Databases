using System.Collections.Generic;
using UnityEngine;

//to allow unity to view such classes as VARIABLES (in engine), we make the class serializable
[System.Serializable]
public class PlayerProfile
{
    public string playerName;
    public string favouriteColourHex;

    public List<DiaryEntry> diaryEntries;
    

    public PlayerProfile(string name, Color color)
    {
        playerName = name;
        favouriteColourHex = ColorUtility.ToHtmlStringRGB(color);
    }
    public PlayerProfile(string name, string favouriteColourHex)
    {
        playerName = name;
        this.favouriteColourHex =favouriteColourHex;
    }
    public Color GetColour()
    {
        Color colour;
        if (ColorUtility.TryParseHtmlString("#" + favouriteColourHex, out colour))
            return colour;

        return Color.white;
    }


}
