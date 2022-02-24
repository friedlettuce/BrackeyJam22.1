using UnityEngine;

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

    [Header ("Costs and User Base")]
    [SerializeField] private int money;
    [SerializeField] private int price;
    [SerializeField] private int hardwarePPU;
    [SerializeField] private int softwarePPU;
    [SerializeField] private int adsPPU;
    [SerializeField] private int serversPPU;
    [SerializeField] private int priceLimit;
    [SerializeField] private int user_base;

    [Header ("Consumer Information")]
    [SerializeField] CityManager city;

    private void Awake(){
        //adsLimit = city.population;
    }
    public float costPriceRatio(){
        // multiply by prices, divide by lowest price
        return hardware * hardwarePPU + software * softwarePPU / price;
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
}
