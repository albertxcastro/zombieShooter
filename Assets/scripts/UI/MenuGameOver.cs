using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGameOver : MonoBehaviour {

    public Button resetButton;
    public string mainSceneName;

	// Use this for initialization
	void Start () {
        resetButton.onClick.AddListener(ReloadLevel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ReloadLevel() {        
        SceneManager.LoadScene(mainSceneName);
        //EventsManager.Instance.BroadcastGameRestart();
    }
}
