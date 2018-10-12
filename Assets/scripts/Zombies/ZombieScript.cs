using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour {

    public float zombieSpeed = 0;    
    public int healthPoints = 10;

    //private int totalHealth;
    //private int zombieID;
    private Rigidbody2D rb;

    void Awake() {
        //zombieID = ZombieIdManager.getZombieID();
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(zombieSpeed, 0);

        //totalHealth = healthPoints;
    }

    void Update()
    {
        rb.velocity = new Vector2(zombieSpeed, 0);

        if (healthPoints <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
    }

    public void ApplyDamage(int damage) {
        healthPoints -= damage;
    }	
}
