using UnityEngine;

public enum Moods
{
    Happy,
    Sad,
    Excited,
    None
}

public class DiaryEntry 
{
    public string title;
    public string description;
    public Moods mood;

}
