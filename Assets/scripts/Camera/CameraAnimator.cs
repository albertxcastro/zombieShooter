using UnityEngine;

public class CameraAnimator : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        EventsManager.Instance.GameOverListener += GameOver;

        animator = GetComponent<Animator>();
	}

    void OnDestroy()
    {
        EventsManager.Instance.GameOverListener += GameOver;
    }

    private void GameOver() {
        animator.SetBool("isDying", true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
