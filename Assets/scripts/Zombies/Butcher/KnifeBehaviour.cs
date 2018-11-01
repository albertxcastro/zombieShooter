using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : MonoBehaviour {

    //publics
    public float knifeSpeed = 1f;
    public float knifeTorque = 1f;
    public GameObject zombieArmParentAnim;

    //privates
    private Rigidbody2D rb;
    private ButcherAnimHandlers butcherAnimHandlers;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        butcherAnimHandlers = zombieArmParentAnim.GetComponent<ButcherAnimHandlers>();

        butcherAnimHandlers.ThrowKnifeListener += Throwned;
    }

    void OnDestroy()
    {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Throwned()
    {
        butcherAnimHandlers.ThrowKnifeListener -= Throwned;
        gameObject.transform.parent = null;
        rb.simulated = true;
        rb.velocity = new Vector2(-knifeSpeed, 0f);
        rb.AddTorque(knifeTorque);
    }
}
