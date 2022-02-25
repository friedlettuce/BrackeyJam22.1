using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;

    private void Awake(){
        source = GetComponent<AudioSource>();

        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null && instance != this)
            Destroy(gameObject);
    }

    public void StartGame(){
        GetComponentInChildren<MusicManager>().StartGame();
    }
    public void StopGame(){
        GetComponentInChildren<MusicManager>().StopGame();
    }

    public void PlaySound(AudioClip _clip){
        source.PlayOneShot(_clip);
    }
}
