using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header ("Buisness Inventory")]
    [SerializeField] public int hardware;
    [SerializeField] public int software;
    [SerializeField] public int ads;
    [SerializeField] public int servers;
    [SerializeField] public int hardwareLimit;
    [SerializeField] public int softwareLimit;
    public int adsLimit;
    public int serversLimit;

    [Header ("Money")]
    [SerializeField] public int money;
    [SerializeField] public int price;
    [SerializeField] public Text moneyText;
    [Header ("Cost")]
    [SerializeField] public int hardwarePPU;
    [SerializeField] public int softwarePPU;
    [SerializeField] public int adsPPU;
    [SerializeField] public int serversPPU;
    [SerializeField] public int priceLimit;
    [Header ("Consumer Info")]
    [SerializeField] public int user_base;
    public HighScore highScore;

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
    public void UpdateSoftware(int units){
        software = units;
    }
    public void UpdateHardware(int units){
        hardware = units;
    }
    public void UpdateServers(int units){
        servers = units;
    }
    public void UpdateAds(int units){
        ads = units;
    }
}
