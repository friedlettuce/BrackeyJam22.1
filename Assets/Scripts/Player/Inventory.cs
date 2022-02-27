using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header ("Buisness Inventory")]
    [SerializeField] public int hardware;
    [SerializeField] public int software;
    [SerializeField] private int userFee;
    [SerializeField] public int ads;
    [SerializeField] public int servers;
    [SerializeField] public int bandwidth;
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
    [SerializeField] private int maintenance;
    [SerializeField] public int priceLimit;
    [Header ("Consumer Info")]
    [SerializeField] private int avgDailyVisit;
    public int user_base;
    public HighScore highScore;

    [Header ("Consumer Information")]
    [SerializeField] CityManager city;
    public int serverDiff;
    [Header ("References")]
    [SerializeField] private Population popManager;

    [Header ("Audio Clips")]
    [SerializeField] private AudioClip ggClip;

    private void Awake(){
        adsLimit = city.GetPopulation() / 10;
        highScore = new HighScore();
        user_base = 0;
        serverDiff = 1;
    }
    public void PricePanel(){
        serverDiff = 0;
        // Takes off percent corresponding to inverse of percent of games
        user_base -= software == softwareLimit ? 0 : (int)(user_base / (1 - (software / softwareLimit)));
        money += user_base * userFee;
        popManager.SetUsers(user_base);
    }
    public void PurchaseInventory(){
        money -= hardware*hardwarePPU + software*softwarePPU + ads*adsPPU + serversPPU*serverDiff;
        UpdateMoney();
    }
    private void UpdateMoney(){
        moneyText.text = "$" + money.ToString();
        if(money <= 0){
            SoundManager.instance.PlaySound(ggClip);
            city.Gameover();
        }
    }
    public void MaintainServers(){
        money -= servers*maintenance;
        UpdateMoney();
    }
    public void Sold(){
        money += price;
        UpdateMoney();
    }
    public void SetPrice(int _price){
        price = _price;
    }
    public float costPriceRatio(){
        int total = hardwarePPU + software*softwarePPU/5 + (int)serversPPU/avgDailyVisit;
        if(price <= 0) return total / .1f;
        return total / price;
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
    public float totalCost(){
        return software * softwarePPU + hardware * hardwarePPU + serverDiff * serversPPU + ads * adsPPU;
    }
    public int MaxSWInc(){
        return (int)((money - (totalCost() - (software * softwarePPU))) / softwarePPU);
    }
    public int MaxHWInc(){
        return (int)((money - (totalCost() - (hardware * hardwarePPU))) / hardwarePPU);
    }
    public int MaxSVRInc(int _serverDiff){
        serverDiff = _serverDiff;
        return (int)((money - (totalCost() - (serverDiff * serversPPU))) / serversPPU);
    }
    public int MaxAdInc(){
        return (int)((money - (totalCost() - (ads * adsPPU))) / adsPPU);
    }
    public bool Vacancy(){
        if(user_base >= servers * bandwidth) return false;
        if(hardware <= 0) return false;
        return true;
    }
    public void NewPurchase(int users){
        user_base = users;
        hardware -= 1;
    }
}
