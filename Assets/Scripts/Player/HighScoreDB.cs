using System.Collections;
using UnityEngine;

public class HighScore{
    public string name;
    public float highscore;

    public HighScore(){
        name = "Garett";
    }

    public string Stringify() 
    {
        return JsonUtility.ToJson(this);
    }
    public static HighScore Parse(string json)
    {
        return JsonUtility.FromJson<HighScore>(json);
    }
}
