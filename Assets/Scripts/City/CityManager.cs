using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CityManager : MonoBehaviour
{
    [Header ("Time")]
    [SerializeField] private int days;
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
    private bool transition;

    [Header ("Population")]
    [SerializeField] private int population;

    [Header ("References")]
    [SerializeField] private Population popManager;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Inventory iv;
    [Header ("GameOver")]
    [SerializeField] private GameObject gameOver;
    [SerializeField] private TextMeshProUGUI msg;
    [SerializeField] private Slider priceSlider;

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
        transition = false;
    }
    void Start(){
        popManager.SetPopulation(population, endOfDay - startOfDay);
        gameOver.SetActive(false);
    }
    private void StopDay(){
        CancelInvoke();
        transition = true;
        iv.MaintainServers();
        InvokeRepeating(nameof(incrementTime), 0f, .01f);
    }
    public void NextDay(){
        InvokeRepeating(nameof(incrementTime), 0f, .1f);
        inventoryPanel.gameObject.SetActive(false);
        iv.PurchaseInventory();
    }
    public void Gameover(){
        CancelInvoke();
        popManager.enabled = false;
        priceSlider.interactable = false;
        int score = iv.user_base * 1000 + iv.money;
        msg.text = "Game Over\n\nThanks for playing!\nHighscore: " + score.ToString();
        if(LoadingManager.instance.highscore < score)
            LoadingManager.instance.highscore = score;
        inventoryPanel.SetActive(false);
        gameOver.SetActive(true);
    }
    public void ReturnToMenu(){
        LoadingManager.instance.TitleScreen();
    }
    private void incrementTime(){
        if(minute++ >= 59){
            ++hour;
            minute = 0;
        }
        if(hour > 23){
            ++day;
            if(day > days) Gameover();
            hour = 0;
            popManager.incrementHappy();
        }
        
        // Displays time, Changes day color
        dayText.text = "Day " + day.ToString();
        hourText.text = (hour < 10 ? " " : "") + hour.ToString();
        minuteText.text = (minute < 10 ? " " : "") + minute.ToString();
        t = ((hour > 11 ? 23 - hour : hour) * 59 + (hour > 11 ? 59 - minute : minute)) / 708f;
        background.color = Color.Lerp(nightColor, dayColor, hour > sunrise && hour < sunset ? t : 0.5f);

        // Spawns consumer
        if(!transition && hour >= startOfDay && hour < endOfDay){
            if(spawnTimer >= spawnTime){
                popManager.SpawnConsumer();
                spawnTimer = 0;
            }
            else
                ++spawnTimer;
        }
        else if(hour > endOfDay && !transition){
            StopDay();
        }
        else if(hour == startOfDay && transition){
            transition = false;
            CancelInvoke();
            inventoryPanel.gameObject.SetActive(true);
            iv.PricePanel();
        }
    }
    public int GetPopulation(){ return population; }
}
