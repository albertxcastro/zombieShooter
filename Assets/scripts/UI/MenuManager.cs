using UnityEngine;

public class MenuManager : MonoBehaviour {

    public CanvasGroup gameOverMenu;
    public CanvasGroup HUD;

    public static MenuManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        //DontDestroyOnLoad(gameObject);
    }

    

    // Use this for initialization
    void Start () {
        EventsManager.Instance.GameOverListener += GameOver;

        gameOverMenu.alpha = 0;
        HUD.alpha = 1;
	}

    void OnDestroy()
    {
        EventsManager.Instance.GameOverListener -= GameOver;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GameOver()
    {
        gameOverMenu.alpha = 1;
        gameOverMenu.blocksRaycasts = true;

        HUD.alpha = 0;
        HUD.blocksRaycasts = false;
    }
}
