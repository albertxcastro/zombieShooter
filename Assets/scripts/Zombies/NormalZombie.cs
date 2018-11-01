using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombie : ZombieScript
{
    //publics
    public Rigidbody2D rightArm;
    public Rigidbody2D leftArm;
    public HingeJoint2D rightLeg;
    public HingeJoint2D leftLeg;

    public float zombieSpeed = 0;

    //privates
    private JointAngleLimits2D limits;

    protected override void OnZombieStart()
    {
        limits = leftLeg.limits;
        limits.min = -30f;
        limits.max = 30f;
    }

    protected override void Die()
    {
        zombieAnimator.SetBool("isDead", true);
        rightArm.simulated = true;
        leftArm.simulated = true;
        leftLeg.limits = limits;
        rightLeg.limits = limits;

        Destroy(gameObject, 5.0f);
    }

    protected override void StopZombieMovement()
    {
        zombieRigidBody.velocity = Vector2.zero;
    }

    protected override void ZombieMovement()
    {
        zombieRigidBody.velocity = new Vector2(zombieSpeed, 0);
    }
}
