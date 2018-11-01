using System;
using UnityEngine;

public abstract class ZombieScript : MonoBehaviour {

    //publics
    public enum ZombieTypeEnum { normalZombie, bigMuncherZombie, butcherZombie };
    public ZombieTypeEnum ZombieType;

    public int healthPoints = 10;

    public Animator zombieAnimator;

    //privates
    private enum Layers { defaultLayer = 0, zombieLayer = 9, deadZombie = 13 };

    //protected methods
    protected bool isGameOver = false;
    protected bool isZombieDead = false;
    protected abstract void ZombieMovement();
    protected abstract void Die();
    protected abstract void StopZombieMovement();
    protected abstract void OnZombieStart();
    

    protected Rigidbody2D zombieRigidBody;

    // Use this for initialization
    protected void Start () {
        zombieRigidBody = GetComponent<Rigidbody2D>();
        EventsManager.Instance.GameOverListener += GameOver;
        OnZombieStart();
    }

    void OnDestroy()
    {
        EventsManager.Instance.GameOverListener -= GameOver;
    }

    void Update()
    {        
        if(healthPoints > 0 && !isGameOver)
        {
            ZombieMovement();
        }
    }

    public void ApplyDamage(int damage) {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            ZombieDie();
        }
    }

    private void ZombieDie()
    {
        EventsManager.Instance.BroadcastZombieDied(ZombieType, transform);
        isZombieDead = true;
        zombieRigidBody.constraints = RigidbodyConstraints2D.None;
        RemoveTagsAndLayers();
        Die();
    }

    private void RemoveTagsAndLayers()
    {
        transform.tag = "Untagged";
        transform.gameObject.layer = (int)Layers.deadZombie;

        foreach (Transform child in transform)
        {
            child.tag = "Untagged";
            child.gameObject.layer = (int)Layers.deadZombie;
            foreach (Transform Schild in child)
            {
                Schild.tag = "Untagged";
                Schild.gameObject.layer = (int)Layers.deadZombie;
            }
        }
    }

    private void GameOver() {
        isGameOver = true;
        StopZombieMovement();
    }
}
