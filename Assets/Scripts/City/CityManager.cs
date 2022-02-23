using UnityEngine;

public class CityManager : MonoBehaviour
{
    [Header ("Population")]
    [SerializeField] private int population;
    
    [Header ("Happiness")]
    [SerializeField] private int happiness;
    [SerializeField] private int personalRange;
    [SerializeField] private int range;
    [SerializeField] private int lowerLimit;
    [SerializeField] private int upperLimit;

    public bool WillBuy(){
        int pH = Mathf.Clamp(happiness + Random.Range(-personalRange, personalRange), lowerLimit, upperLimit);
        return pH > happiness;
    }
    public int GenHappy(int previous){
        int range_l = previous > lowerLimit + range ? range : previous  - lowerLimit;
        int range_r = previous < upperLimit - range ? range : upperLimit - previous;
        return Random.Range(previous - range_l, previous + range_r);
    }
}
