using UnityEngine;

public class FollowCursorGun : MonoBehaviour
{

    //public Transform balaSpawn;
    public Vector3 balaSpawnGlobalRotation;

    public float minAngle = -100f;
    public float maxAngle = 100f;
    public float positionXFix = 0f;

    public float localRotationZ;

    public Vector2 direction;
    private float angle;
    private Quaternion rotation;
    public float angleFix = 0;

    private Transform gun;

    private Quaternion newRotation;
    private bool isGameOver = false;

    void Start()
    {
        EventsManager.Instance.SubscribePointToCursor(PointToCursor);
        EventsManager.Instance.GameOverListener += GameOver;

        gun = transform;
    }

    void OnDestroy()
    {
        EventsManager.Instance.GameOverListener -= GameOver;
    }

    // Use this for initialization
    void FixedUpdate()
    {
        if (!isGameOver)
        {
            PointToCursor();
        }
    }

    private void PointToCursor()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gun.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angleFix;        

        newRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        gun.rotation = newRotation;

        localRotationZ = gun.localEulerAngles.z;

        
        float newAngleZ = Mathf.Clamp(gun.localEulerAngles.z, minAngle, maxAngle);
        gun.localEulerAngles = new Vector3(0, 0, newAngleZ);
        
    }

    private void GameOver()
    {
        isGameOver = true;
        
        gun.localEulerAngles = new Vector3(0, 0, 270f);
        
    }
}
