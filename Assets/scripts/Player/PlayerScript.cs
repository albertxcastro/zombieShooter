using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private bool isPointerOnUI = false;
    private bool isShooting = false;
    private bool isGameOver = false;
    // Use this for initialization
    void Start () {
        EventsManager.Instance.OverUIListener += PointerOnUI;
        EventsManager.Instance.GameOverListener += GameOver;
    }

    private void GameOver()
    {
        isGameOver = true;
        EventsManager.Instance.BroadcastPlayerStopedShooting();
    }

    void OnDestroy()
    {
        EventsManager.Instance.OverUIListener -= PointerOnUI;
    }
	
	// Update is called once per frame
	void Update () {

        if (isGameOver)
        {
            return;
        }

        if(!isShooting && Input.GetMouseButtonDown(0) && !isPointerOnUI)
        {
            isShooting = true;
            EventsManager.Instance.BroadcastPlayerIsShooting();
        }

        if (isShooting && Input.GetMouseButtonUp(0))
        {
            isShooting = false;
            EventsManager.Instance.BroadcastPlayerStopedShooting();
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "zombie" || col.tag == "Throwable")
        {
            EventsManager.Instance.BroadcastGameOver();
        }
    }


    private void PointerOnUI(bool pointerOnUI)
    {
        isPointerOnUI = pointerOnUI;
    }
}
