using System;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour {

    public static EventsManager Instance { get; private set; }

    private List<Action> subsPointToCursor = new List<Action>();

    public delegate void gameOver();
    public event gameOver GameOverListener;

    public delegate void overUIElement(bool isOverUI);
    public event overUIElement OverUIListener;

    public delegate void updateBulletsCounterUI();
    public event updateBulletsCounterUI UpdateBulletsCounterUIListener;

    public delegate void switchGun(GunBehaviour gunBehaviour);
    public event switchGun SwitchGunListener;

    //TODO puedes hacer que estos sean el mismo evento
    public delegate void playerIsShooting();
    public event playerIsShooting PlayerIsShootingListener;

    public delegate void playerStopedShooting();
    public event playerStopedShooting PlayerStopedShootingListener;

    public delegate void zombieDied(ZombieScript.ZombieTypeEnum zombieType, Transform zombiePosition);
    public event zombieDied ZombieDiedListener;

    void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;

        //DontDestroyOnLoad(gameObject);
    }
    
    public void SubscribePointToCursor(Action subscriber)
    {
        subsPointToCursor.Add(subscriber);
    }
    //The Unsubscribe method for manager
    public void UnSubscribePointToCursor(Action subscriber)
    {
        subsPointToCursor.Remove(subscriber);
    }
    
    public void BroadcastPointToCursor()
    {
        foreach (var subscriber in subsPointToCursor)
        {
            subscriber();
        }
    }

    public void BroadcastGameOver()
    {
        if (GameOverListener != null)
        {
            GameOverListener();
        }
    }    
    
    public void BroadcastPointerOnUI(bool isOverUI) {
        if (OverUIListener != null)
        {
            OverUIListener(isOverUI);
        }
    }

    public void BroadcastUpdateBulletsCounter() {
        if (UpdateBulletsCounterUIListener != null)
        {
            UpdateBulletsCounterUIListener();
        }
    }

    public void BroadcastSwitchGun(GunBehaviour gunBehaviour)
    {
        if (SwitchGunListener != null)
        {
            SwitchGunListener(gunBehaviour);
        }
    }

    public void BroadcastPlayerIsShooting()
    {
        if (PlayerIsShootingListener != null)
        {
            PlayerIsShootingListener();
        }
    }

    public void BroadcastPlayerStopedShooting()
    {
        if (PlayerStopedShootingListener != null)
        {
            PlayerStopedShootingListener();
        }
    }

    public void BroadcastZombieDied(ZombieScript.ZombieTypeEnum zombieType, Transform zombiePosition)
    {
        if (ZombieDiedListener != null)
        {
            ZombieDiedListener(zombieType, zombiePosition);
        }
    }
}
