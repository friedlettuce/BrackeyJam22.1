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

    [Header ("References")]
    [SerializeField] private Inventory inventory;

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
    public void incrementHappy(){
        happiness = Mathf.Clamp(Random.Range(happiness - range, happiness + range), lowerLimit, upperLimit);
    }
    public float HappyValue(){
        return Mathf.Clamp(Random.Range(happiness - range, happiness + range), lowerLimit, upperLimit);
    }
    public bool WillBuy(float _happiness){
        return Random.Range(1, 101) <= inventory.costPriceRatio() * cpBuyMult + (
            1 - _happiness / upperLimit) * happyBuyMult + inventory.adsRatio() * adsBuyMult;
    }
    public bool SoftOrHard(){
        return inventory.SoftOrHard();
    }
    public bool Enjoyed(float experience, float _happiness){
        return experience > _happiness;
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
}
