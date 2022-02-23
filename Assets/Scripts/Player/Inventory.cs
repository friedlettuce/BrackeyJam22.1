using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header ("Buisness Inventory")]
    [SerializeField] private int hardware;
    [SerializeField] private int software;
    [SerializeField] private int ads;
    [SerializeField] private int servers;

    [Header ("Costs and User Base")]
    [SerializeField] private int money;
    [SerializeField] private int price;
    [SerializeField] private int user_base;
}
