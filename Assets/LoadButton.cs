using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    private Button button;
    private void Awake(){
        button = GetComponent<Button>();
    }
    private void Start(){
        button.onClick.AddListener(LoadingManager.instance.PlayGame);
    }
}
