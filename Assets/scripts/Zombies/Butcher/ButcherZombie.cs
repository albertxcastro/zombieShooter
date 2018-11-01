using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButcherZombie : ZombieScript {

    //publics
    public float zombieSpeed = 0f;
    public ButcherAnimHandlers butcherAnimHandlers;
    public float throwKnifeCooldown = 1f;

    //privates
    private bool isThrowingKnife = false;

    protected override void OnZombieStart()
    {
        butcherAnimHandlers.ThrowKnifeListener += KnifeHasBeenThrown;
    }

    void OnDestroy()
    {
        butcherAnimHandlers.ThrowKnifeListener -= KnifeHasBeenThrown;
    }

    private void KnifeHasBeenThrown()
    {
        zombieAnimator.SetBool("isThrowingKnife", false);
        isThrowingKnife = false;
    }

    protected override void Die()
    {

    }

    protected override void StopZombieMovement()
    {
        zombieRigidBody.velocity = Vector2.zero;
    }

    protected override void ZombieMovement()
    {
        zombieRigidBody.velocity = new Vector2( zombieSpeed, 0f);

        if (!isThrowingKnife)
        {
            StartCoroutine("ThrowKnifeTimer");
        }
    }

    IEnumerator ThrowKnifeTimer()
    {
        isThrowingKnife = true;
        yield return new WaitForSecondsRealtime(throwKnifeCooldown);
        ThrowKnife();
    }

    private void ThrowKnife()
    {
        zombieAnimator.SetBool("isThrowingKnife", true);
    }
}
