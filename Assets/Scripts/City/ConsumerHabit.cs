using UnityEngine;

public class ConsumerHabit : MonoBehaviour
{
    public float happiness { get; private set; }

    public void SetHappiness(){
        happiness = transform.parent.GetComponent<Population>().HappyValue();
    }
}
