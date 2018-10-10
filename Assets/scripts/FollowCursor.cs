using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour {

    //public Transform balaSpawn;
    public Vector3 balaSpawnGlobalRotation;

    public Vector2 direction;
    public float angle;
    public Quaternion rotation;
    public float angleFix = 0;
    public Vector3 mousePosition;
    public bool clampPlayer = false;
    public bool clampGun = false;

    void Start() {
        PointToCursorManager.Instance.Subscribe(PointToCursor);
    }

    // Use this for initialization
    void FixedUpdate ()
    {
        PointToCursor();
    }

    private void PointToCursor()
    {
        //balaSpawnGlobalRotation = balaSpawn.rotation.eulerAngles;
        //direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - balaSpawn.position;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angleFix;
        if (clampPlayer)
        {
            angle = Mathf.Clamp(angle, 10f, 180f);
        }
        if (clampGun)
        {
            angle = Mathf.Clamp(angle, -100f, 100f);
        }
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }
}
