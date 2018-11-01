using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMuncherZombie : ZombieScript
{
    //public
    public float zombieYForce = 1f;
    public float zombieXForce = 1f;
    public float jumpCooldownTime = 1f;
    public float forceOnDeath = 1f;
    public Vector2 centerOfMass;

    //private
    
    private bool isJumping = false;
    private bool isOnGround = false;
    private bool isJumpInCooldown = false;
    private float yVelocity;

    protected override void OnZombieStart()
    {
        zombieAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        yVelocity = zombieRigidBody.velocity.y;
        if (yVelocity < 0 && !isZombieDead)
        {
            zombieAnimator.SetBool("isFalling", true);
        }
    }

    protected override void Die()
    {
        zombieAnimator.SetBool("isDead", true);
        zombieRigidBody.mass = 1f;
        zombieRigidBody.drag = 0.5f;
        zombieRigidBody.angularDrag = 0f;
        zombieRigidBody.centerOfMass = centerOfMass;

        zombieRigidBody.AddTorque(forceOnDeath);

        //zombieRigidBody.centerOfMass = new Vector2(0f, 0.9f); ;


        Destroy(gameObject, 5f);
    }

    protected override void StopZombieMovement()
    {
        //nada necesario
    }

    protected override void ZombieMovement()
    {
        if (!isJumping && isOnGround && !isJumpInCooldown)
        {
            StartCoroutine("JumpCooldown");
        }
    }

    IEnumerator JumpCooldown()
    {
        isJumpInCooldown = true;
        yield return new WaitForSecondsRealtime(jumpCooldownTime);

        if (!isGameOver && !isZombieDead)
        {
            ZombieJump();
        }
    }

    private void ZombieJump()
    {
        isJumping = true;
        isOnGround = false;
        zombieRigidBody.AddForce(new Vector2(zombieXForce, zombieYForce));

        zombieAnimator.SetBool("isAboutToJump", false);
        zombieAnimator.SetBool("isJumping", true);
        zombieAnimator.SetBool("isFalling", false);
        zombieAnimator.SetBool("didHitTheGround", false);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" && !isZombieDead)
        {
            isOnGround = true;
            isJumping = false;
            isJumpInCooldown = false;

            zombieAnimator.SetBool("isAboutToJump", true);
            zombieAnimator.SetBool("isJumping", false);
            zombieAnimator.SetBool("isFalling", false);
            zombieAnimator.SetBool("didHitTheGround", true);
        }
    }


}
