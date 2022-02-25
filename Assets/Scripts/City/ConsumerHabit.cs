using System.Collections;
using UnityEngine;

public class ConsumerHabit : MonoBehaviour
{
    [Header ("Reaction")]
    [SerializeField] private int spawnLimit;
    [SerializeField] private int lingerTime;
    [SerializeField] private GameObject softwarePrefab;
    [SerializeField] private GameObject hardwarePrefab;
    [SerializeField] private GameObject reaction;
    public float happiness { get; private set; }

    public IEnumerator LowSoftware(){
        yield return new WaitForSeconds(Random.Range(0, spawnLimit));
        reaction = Instantiate(softwarePrefab, new Vector3(
            transform.position.x, transform.position.y + .8f, 1), Quaternion.identity);
        yield return new WaitForSeconds(lingerTime);
        Destroy(reaction);
    }
    public IEnumerator LowHardware(){
        yield return new WaitForSeconds(Random.Range(0, spawnLimit));
        reaction = Instantiate(hardwarePrefab, new Vector3(
            transform.position.x, transform.position.y + .8f, 1), Quaternion.identity);
        yield return new WaitForSeconds(lingerTime);
        Destroy(reaction);
    }

    private void Update(){
        if(reaction != null){
            reaction.transform.position = new Vector3(
                transform.position.x, transform.position.y + .8f, transform.position.z);
        }
    }

    public void SetHappiness(){
        happiness = transform.parent.GetComponent<Population>().HappyValue();
    }
}
