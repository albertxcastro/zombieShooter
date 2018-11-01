using UnityEngine;

public class Gun9mm : GunBehaviour, IGun
{
    //private
    private AudioSource[] bulletFX;

    protected override AudioSource[] BulletFX
    {
        get
        {
            return bulletFX;
        }
    }    

    // Use this for initialization
    new void Start () {

        base.Start();

        bulletFX = GetComponents<AudioSource>();

    }  
}
