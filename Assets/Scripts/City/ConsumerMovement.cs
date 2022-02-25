using System.Collections;
using UnityEngine;

public class ConsumerMovement: MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    private bool walking;
    private float happiness;
    private bool passDoor;
    private float rightSpawn;
    private float leftSpawn;
    private float direction;

    private BoxCollider2D boxCollider;
    private Animator anim;
    private Vector3 originalScale;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        originalScale = transform.localScale;
        passDoor = false;
        walking = true;
    }
    void Start(){
        anim.SetBool("walking", walking);
    }

    private void Update()
    {
        if(direction > 0 && transform.position.x > rightSpawn) gameObject.SetActive(false);
        else if(direction < 0 && transform.position.x < leftSpawn) gameObject.SetActive(false);

        float movementSpeed = walking ? speed * Time.deltaTime * direction : 0;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Door" || passDoor) return;

        boxCollider.enabled = false;
        passDoor = true;
        anim.SetBool("walking", false);
        walking = false;
        StartCoroutine(BuyVR());
    }

    private IEnumerator BuyVR(){
        float startTime = Time.time;
        StartCoroutine(InStore());
        yield return new WaitForSeconds(waitTime);
        anim.SetBool("walking", true);
        walking = true;
    }
    private IEnumerator InStore(){
        for(float st = Time.time; (Time.time - st) / (waitTime / 2) <= 1;){
            sprite.color = new Color(1f,1f,1f,Mathf.SmoothStep(
                0f, 1f, (Time.time - st) / (waitTime / 2)));
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void SetDirection(float _direction, float _leftSpawn, float _rightSpawn)
    {
        gameObject.SetActive(true);
        direction = _direction; //-1 or 1
        leftSpawn = _leftSpawn;
        rightSpawn = _rightSpawn;
        passDoor = false;
        boxCollider.enabled = true;
        
        transform.position = new Vector3(
            direction > 0 ? leftSpawn : rightSpawn, transform.position.y, transform.position.z);
        transform.localScale = new Vector3(
            Mathf.Abs(transform.localScale.x)*direction, transform.localScale.y, transform.localScale.z);

        anim.SetBool("walking", true);
    }
    void OnDisable(){
        transform.localScale = transform.parent.localScale;
        transform.localScale = new Vector3(transform.localScale.x*2, transform.localScale.y*2, 1);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}