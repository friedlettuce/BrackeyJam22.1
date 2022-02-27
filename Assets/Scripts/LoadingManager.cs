using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] SoundManager sound;
    public static LoadingManager instance { get; private set; }
    public float highscore;

    private void Awake()
    {
        highscore = 0;

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayGame()
    {
        sound.StartGame();
        SceneManager.LoadScene(1);
    }
    public void TitleScreen()
    {
        sound.StopGame();
        SceneManager.LoadScene(0);
    }
    public void SetScore(float _highscore){
        highscore = _highscore;
    }
}