using UnityEngine;
using Random = UnityEngine.Random;

public class BulletScript : MonoBehaviour {

    public float bulletSpeed = 1;
    public float bulletSelfDestroyTime = 1f;
    public float bulletCriticalChance = 0.05f;
    public int bulletCriticalDamage = 3;
    public int bulletDamage = 2;
    public int bulletHeadDamage = 4;


    public GameObject bloodEffect;

    private Rigidbody2D rb;
    private Vector3 rot;
    private bool isColliding = false;
    // Use this for initialization

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;

        rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 90, rot.z);

        Destroy(gameObject, bulletSelfDestroyTime);
    }

    void OnCollisionEnter2D(Collision2D col) {
        //col.gameObject.SendMessage("ApplyDamage", bulletDamage, SendMessageOptions.DontRequireReceiver);
        //Debug.Log(col.gameObject.tag);

        if (isColliding) return;
        isColliding = true;

        if (col.gameObject.tag == "zombie") {

            ZombieScript zs = col.transform.root.GetComponent<ZombieScript>();

            if (zs != null)
            {
                int criticalMult = Random.Range(0f, 1f) < bulletCriticalChance ? bulletCriticalDamage : 1;

                if (col.gameObject.name == "weak spot")
                {
                    zs.ApplyDamage(bulletHeadDamage * criticalMult);
                }
                else
                {
                    zs.ApplyDamage(bulletDamage * criticalMult);
                }
                
            }

            Instantiate(bloodEffect, transform.position, Quaternion.Euler(rot));
        }

        Destroy(gameObject);
    }
}
