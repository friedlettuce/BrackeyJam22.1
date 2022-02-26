using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header ("Buisness Inventory")]
    [SerializeField] private int hardware;
    [SerializeField] private int software;
    [SerializeField] private int ads;
    [SerializeField] private int servers;
    [SerializeField] private int hardwareLimit;
    [SerializeField] private int softwareLimit;
    private int adsLimit;
    private int serversLimit;

    [Header ("Money")]
    [SerializeField] private int money;
    [SerializeField] private int price;
    [SerializeField] private Text moneyText;
    [Header ("Cost")]
    [SerializeField] private int hardwarePPU;
    [SerializeField] private int softwarePPU;
    [SerializeField] private int adsPPU;
    [SerializeField] private int serversPPU;
    [SerializeField] private int priceLimit;
    [Header ("Consumer Info")]
    [SerializeField] private int user_base;
    private HighScore highScore;

    [Header ("Panel References")]
    [SerializeField] private Slider softwareSlider;

    [Header ("Consumer Information")]
    [SerializeField] CityManager city;

    private void Awake(){
        adsLimit = city.GetPopulation() / 10;
        highScore = new HighScore();
    }
    private void UpdateMoney(){
        moneyText.text = "$" + money.ToString();
    }
    public void Sold(){
        money += price;
        UpdateMoney();
    }
    public void SetPrice(int _price){
        price = _price;
    }
    public float costPriceRatio(){
        if(price <= 0) return hardware*hardwarePPU + software*softwarePPU / .1f;
        return (hardware * hardwarePPU + software * softwarePPU) / price;
    }
    public float experience(){
        return 60;
    }
    public float adsRatio(){
        return ads / adsLimit;
    }
    public float adsPercent(){
        return 1f;
    }
    public bool SoftOrHard(){
        return software / softwareLimit < hardware / hardwareLimit;
    }
}
