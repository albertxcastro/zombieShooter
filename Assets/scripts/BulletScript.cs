using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public float bulletSpeed = 1;
    public float bulletSelfDestroyTime = 1f;
    public int bulletDamage = 2;
    public int bulletHeadDamage = 4;

    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;

        Destroy(gameObject, bulletSelfDestroyTime);
    }

    void OnCollisionEnter2D(Collision2D col) {
        //col.gameObject.SendMessage("ApplyDamage", bulletDamage, SendMessageOptions.DontRequireReceiver);
        ZombieScript zs = col.gameObject.GetComponent<ZombieScript>();

        if (zs != null)
        {
            zs.ApplyDamage(bulletDamage);            
        }

        Destroy(gameObject);
    }
}
