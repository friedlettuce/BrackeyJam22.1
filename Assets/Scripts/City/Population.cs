using UnityEngine;

public class Population : MonoBehaviour
{   
    [Header ("Population")]
    [SerializeField] private GameObject[] consumers;
    [SerializeField] private GameObject consumerPrefab;
    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;

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

    public void SpawnConsumer(){
        bool dir = Random.value >= 0.5f;
        int fc = FindConsumer();

        consumers[fc].GetComponent<ConsumerMovement>().SetDirection(
            dir ? 1f : -1f, leftSpawn.position.x, rightSpawn.position.x);
    }
    private int FindConsumer()
    {
        for(int c = 0; c < consumers.Length; ++c)
        {
            if (!consumers[c].activeInHierarchy)
            {
                return c;
            }
        }
        return 0;
    }
    public void SetPopulation(int population){
        population += 5;
        if(population < 0) population = 0;

        consumers = new GameObject[population];
        for(int i = 0; i < population; ++i){
            consumers[i] = (GameObject)Instantiate(
                consumerPrefab, new Vector3(0, leftSpawn.position.y, 0), Quaternion.identity);
            consumers[i].transform.parent = transform;
            consumers[i].SetActive(false);
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
