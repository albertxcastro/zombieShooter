using System;
using UnityEngine;

public class FollowCursorPlayer : MonoBehaviour {

    //public Transform balaSpawn;
    public Vector3 balaSpawnGlobalRotation;

    public float minAngle = -100f;
    public float maxAngle = 100f;

    public float localRotationZ;

    public Vector2 direction;
    public float angle;
    public Quaternion rotation;
    public float angleFix = 0;
    public Vector3 mousePosition;

    private Transform arm;

    private Quaternion newRotation;
    private bool isGameOver = false;
    private bool isReloading = false;

    void Start() {
        EventsManager.Instance.SubscribePointToCursor(PointToCursor);
        EventsManager.Instance.GameOverListener += GameOver;
        GunBehaviour.IsReloadingListener += IsPlayerReloading;

        arm = transform;
    }

    private void IsPlayerReloading(bool reloading)
    {
        isReloading = reloading;
    }

    void OnDestroy()
    {
        EventsManager.Instance.GameOverListener -= GameOver;
        GunBehaviour.IsReloadingListener -= IsPlayerReloading;
    }

    // Use this for initialization
    void FixedUpdate ()
    {
        if (!isGameOver && !isReloading)
        {
            PointToCursor();
        }

        if (isReloading)
        {
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
        }
    }

    private void PointToCursor()
    {
        if (isReloading)
        {
            return;
        }
        //balaSpawnGlobalRotation = balaSpawn.rotation.eulerAngles;
        //direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - balaSpawn.position;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - arm.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angleFix;
        
        angle = Mathf.Clamp(angle, 10f, 180f);

        newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                
        arm.rotation = newRotation;

        localRotationZ = arm.localEulerAngles.z;

    }

    private void GameOver() {
        isGameOver = true;

        arm.localEulerAngles = new Vector3(0, 0, 36f);
    }
}
