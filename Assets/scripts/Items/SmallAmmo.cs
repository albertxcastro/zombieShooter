using System;
using UnityEngine;

public class SmallAmmo : MonoBehaviour {

    public float itemVelocity = 1f;

    private bool isDestroying = false;
    private GunBehaviour gun;
    private Rigidbody2D rb;

    void Start()
    {
        EventsManager.Instance.SwitchGunListener += SwitchedGun;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * itemVelocity;
    }

    void OnDestroy()
    {
        EventsManager.Instance.SwitchGunListener -= SwitchedGun;
    }

    private void SwitchedGun(GunBehaviour gunBehaviour)
    {
        gun = gunBehaviour;
    }

    // Use this for initialization
    void OnMouseDown() {
        if (!isDestroying)
        {
            if (gun == null)
            {
                gun = GunsManager.currentGun.GetComponent<GunBehaviour>();
            }

            ItemSpawner.ammoOnScene--;

            gun.AddAmmo(10);
            
            EventsManager.Instance.BroadcastUpdateBulletsCounter();
            isDestroying = true;
            Destroy(gameObject, 0.2f);
        }
    }



}
