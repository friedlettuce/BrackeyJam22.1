using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    private void Start(){
        GetComponent<TextMeshProUGUI>().text = "Highscore: " + LoadingManager.instance.highscore.ToString();
    }
}
