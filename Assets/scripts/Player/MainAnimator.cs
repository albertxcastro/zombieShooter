using System;
using UnityEngine;

public class MainAnimator : MonoBehaviour {

    //privates
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        GunBehaviour.IsReloadingListener += IsPlayerReloading;
        EventsManager.Instance.GameOverListener += GameOver;
	}

    void OnDestroy()
    {
        GunBehaviour.IsReloadingListener -= IsPlayerReloading;
        EventsManager.Instance.GameOverListener -= GameOver;
    }

    private void IsPlayerReloading(bool reloading)
    {
        if (reloading)
        {
            animator.SetBool("isReloading", true);
        }
        else
        {
            animator.SetBool("isReloading", false);
        }
    }

    private void GameOver() {
        animator.SetBool("isDying", true);
    }

}
