using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public float bulletSpeed = 1;
    public float bulletSelfDestroyTime = 1f;
    public int bulletDamage = 2;
    public int bulletHeadDamage = 4;
    public GameObject bloodEffect;

    private Rigidbody2D rb;
    private Vector3 rot;
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
        if (col.gameObject.tag == "zombie") {

            ZombieScript zs = col.gameObject.GetComponent<ZombieScript>();

            if (zs != null)
            {
                zs.ApplyDamage(bulletDamage);
            }

            Instantiate(bloodEffect, transform.position, Quaternion.Euler(rot));
        }

        Destroy(gameObject);
    }
}
