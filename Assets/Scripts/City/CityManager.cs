using UnityEngine;
using UnityEngine.UI;

public class CityManager : MonoBehaviour
{
    [Header ("Time")]
    [SerializeField] private int startOfDay;
    [SerializeField] private int endOfDay;
    private int openTime;
    private int spawnTime;
    private int spawnTimer;
    [SerializeField] private int sunrise;
    [SerializeField] private int sunset;

    [Header ("Time Display")]
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private Text dayText;
    [SerializeField] private Text hourText;
    [SerializeField] private Text minuteText;
    [SerializeField] private Color dayColor;
    [SerializeField] private Color nightColor;
    private int day;
    private int hour;
    private int minute;
    private float t;

    [Header ("Population")]
    [SerializeField] private int population;

    [Header ("References")]
    [SerializeField] private Population popManager; 

    private void Awake(){
        day = 1;
        hour = 8;
        minute = 0;

        openTime = (endOfDay - startOfDay) * 60;
        if(population > openTime){
            population = openTime;
            spawnTime = 1;
        }
        else{
            spawnTime = (int)(openTime / population);
        }
        spawnTimer = 0;
    }
    void Start(){
        popManager.SetPopulation(population / (endOfDay - startOfDay));
        InvokeRepeating(nameof(incrementTime), 1f, .1f);
    }
    private void incrementTime(){
        if(minute++ >= 59){
            ++hour;
            minute = 0;
        }
        if(hour > 23){
            ++day;
            hour = 0;
        }
        
        // Displays time, Changes day color
        dayText.text = "Day " + day.ToString();
        hourText.text = (hour < 10 ? " " : "") + hour.ToString();
        minuteText.text = (minute < 10 ? " " : "") + minute.ToString();
        t = ((hour > 11 ? 23 - hour : hour) * 59 + (hour > 11 ? 59 - minute : minute)) / 708f;
        background.color = Color.Lerp(nightColor, dayColor, hour > sunrise && hour < sunset ? t : 0.5f);

        // Spawns consumer
        if(hour >= startOfDay && hour < endOfDay){
            if(spawnTimer >= spawnTime){
                popManager.SpawnConsumer();
                spawnTimer = 0;
            }
            else
                ++spawnTimer;
        }
    }
    public int GetPopulation(){ return population; }
}
