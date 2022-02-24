using UnityEngine;

public class Population : MonoBehaviour
{   
    [Header ("Population")]
    [SerializeField] private GameObject[] consumers;

    [Header ("Happiness")]
    [SerializeField] private int happiness;
    [SerializeField] private int personalRange;
    [SerializeField] private int range;
    [SerializeField] private int lowerLimit;
    [SerializeField] private int upperLimit;

    [Header ("Equation Modifiers")]
    [SerializeField] private float happyBuyMult;
    [SerializeField] private float adsBuyMult;
    [SerializeField] private float cpBuyMult;
    private int pH;

    public void SetPopulation(int population){
        if(population < 0) population = 0;
        Debug.Log(population);
        consumers = new GameObject[population];
        for(int i = 0; i < population; ++i){
            consumers[i] = new GameObject();
            consumers[i].transform.parent = transform;
        }
    }
    public bool WillBuy(float cpr, float adsr){
        pH = Mathf.Clamp(happiness + Random.Range(-personalRange, personalRange), lowerLimit, upperLimit);
        float chance = cpr * cpBuyMult + (1 - pH / upperLimit) * happyBuyMult + adsr * adsBuyMult;
        return Random.Range(1, 100) <= chance;
    }
    public bool Enjoyed(float experience){
        return experience > pH;
    }
    public void incrementHappy(){
        happiness = Mathf.Clamp(Random.Range(happiness - range, happiness + range), lowerLimit, upperLimit);
    }
}
