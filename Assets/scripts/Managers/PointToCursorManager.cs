using System;
using System.Collections.Generic;
using UnityEngine;

public class PointToCursorManager : MonoBehaviour {

    public static PointToCursorManager Instance { get; private set; }

    private List<Action> subscribers = new List<Action>();

    void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    
    public void Subscribe(Action subscriber)
    {
        subscribers.Add(subscriber);
    }
    //The Unsubscribe method for manager
    public void UnSubscribe(Action subscriber)
    {
        subscribers.Remove(subscriber);
    }
    //Clear subscribers method for manager
    public void ClearAllSubscribers()
    {
        subscribers.Clear();
    }

    public void Broadcast()
    {
        foreach (var subscriber in subscribers)
        {
            subscriber();
        }
    }
}
