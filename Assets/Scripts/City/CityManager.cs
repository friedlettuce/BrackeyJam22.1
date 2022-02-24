using UnityEngine;

public class CityManager : MonoBehaviour
{
    [Header ("Time")]
    [SerializeField] private Camera cam;
    [SerializeField] private Color dayColor;
    [SerializeField] private Color nightColor;
    private int day;
    private int hour;
    private int minute;
    private float t;

    [Header ("Population")]
    [SerializeField] private int population;

    [Header ("References")]
    [SerializeField] private Inventory vr;
    private Population popManager; 

    private void Awake(){
        day = 1;
        hour = 8;
        minute = 0;
        popManager = GetComponentInChildren<Population>();
        InvokeRepeating(nameof(incrementTime), 1f, .01f);
    }
    private void incrementTime(){
        if(minute++ >= 59){
            ++hour;
            minute = 0;
        }
        if(hour >= 23){
            ++day;
            hour = 0;
        }
        t = ((hour > 11 ? 23 - hour : hour) - 5) / 11f;
        cam.backgroundColor = Color.Lerp(nightColor, dayColor, hour > 5 ? t : 0f);
    }
}
