using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButcherAnimHandlers : MonoBehaviour {

    //publics
    public GameObject knife;
    public Transform armParent;
    public delegate void throwKnife();
    public event throwKnife ThrowKnifeListener;

    //privates
    private GameObject newKnife;

    void ActivateKnife()
    {
        newKnife = Instantiate(knife, armParent);
        newKnife.SetActive(true);
    }

    void ThrowKnife()
    {
        ThrowKnifeListener();
    }
}
