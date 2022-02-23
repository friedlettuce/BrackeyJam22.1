using UnityEngine;

public class CityManager : MonoBehaviour
{
    [Header ("Population")]
    [SerializeField] public int population { get; private set; }
    
    [Header ("Happiness")]
    [SerializeField] private int happiness;
    [SerializeField] private int personalRange;
    [SerializeField] private int range;
    [SerializeField] private int lowerLimit;
    [SerializeField] private int upperLimit;

    [Header ("Inventory")]
    [SerializeField] private Inventory vr;
    [SerializeField] private int pcMultiplier;

    public bool WillBuy(){
        int pH = Mathf.Clamp(happiness + Random.Range(-personalRange, personalRange), lowerLimit, upperLimit);
        return vr.ads + vr.costPriceRatio() * pcMultiplier;
    }
    public int GenHappy(int previous){
        int range_l = previous > lowerLimit + range ? range : previous  - lowerLimit;
        int range_r = previous < upperLimit - range ? range : upperLimit - previous;
        return Random.Range(previous - range_l, previous + range_r);
    }
}
