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
    [SerializeField] private int serversLimit;

    [Header ("Costs and User Base")]
    [SerializeField] private int money;
    [SerializeField] private int price;
    [SerializeField] private int priceLimit;
    [SerializeField] private int user_base;

    [Header ("Consumer Information")]
    [SerializeField] CityManager city;

    private void Awake(){
        adsLimit = city.population;
    }
    public float costPriceRatio(){
        return hardware + software / price;
    }
    public float adsPercent(){

    }
}
