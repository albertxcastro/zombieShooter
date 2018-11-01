using System;
using System.Collections;
using UnityEngine;

public abstract class GunBehaviour : MonoBehaviour {

    //publics
    public float bulletCooldownTime = 0.5f;
    public float reloadingTime = 2.5f;
    public int totalBullets = 8;
    public int bulletsGunCapacity = 8;

    public GameObject bulletPrefab;

    public delegate void isReloadingDelegate(bool reloading);
    public static event isReloadingDelegate IsReloadingListener;

    //protecteds
    protected bool isShooting = false;
    protected bool isInCooldown = false;
    protected bool isReloading = false;
    
    protected abstract AudioSource[] BulletFX { get; }

    //privates
    private int bulletsLoaded;
    //private bool isGameOver = false;

    // Use this for initialization
    protected void Start () {
        EventsManager.Instance.PlayerIsShootingListener += PlayerIsShooting;
        EventsManager.Instance.PlayerStopedShootingListener += PlayerStopedShooting;
        //EventsManager.Instance.GameOverListener += PlayerGameOver;

        bulletsLoaded =  bulletsGunCapacity;
        EventsManager.Instance.BroadcastSwitchGun(gameObject.GetComponent<GunBehaviour>());
        UpdateBulletsUI();
    }

    //private void PlayerGameOver()
    //{
    //    throw new NotImplementedException();
    //}

    private void PlayerStopedShooting()
    {
        isShooting = false;
    }

    private void PlayerIsShooting()
    {
        isShooting = true;
    }

    void OnDestroy()
    {
        EventsManager.Instance.PlayerIsShootingListener -= PlayerIsShooting;
        EventsManager.Instance.PlayerStopedShootingListener -= PlayerStopedShooting;
    }

    // Update is called once per frame
    void Update () {
        
    }

    void FixedUpdate()
    {
        if (isShooting
            && !isInCooldown
            && bulletsLoaded > 0
            && !isReloading)
        {
            Shoot();
        }

        if (bulletsLoaded == 0 && !isReloading && totalBullets > 0)
        {
            Reload();
        }
    }

    private void Reload()
    {
        IsReloadingListener(true);

        BulletFX[2].Play();

        StartCoroutine("ReloadingCooldown");

        if (totalBullets >= bulletsGunCapacity)
        {
            bulletsLoaded = bulletsGunCapacity;
        }
        else
        {
            bulletsLoaded = totalBullets;
        }

        totalBullets -= bulletsLoaded;
    }

    IEnumerator ReloadingCooldown()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadingTime);
        isReloading = false;
        UpdateBulletsUI();
        IsReloadingListener(false);
    }

    private void Shoot()
    {
        StartCoroutine("BulletCooldown");

        bulletsLoaded--;

        UpdateBulletsUI();

        EventsManager.Instance.BroadcastPointToCursor();

        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.SetActive(true);

        if (BulletFX[0].isPlaying)
        {
            if (BulletFX[1].isPlaying)
            {
                BulletFX[0].Play();
            }
            else
            {
                BulletFX[1].Play();
            }
        }
        else
        {
            BulletFX[0].Play();
        }

    }

    IEnumerator BulletCooldown()
    {
        isInCooldown = true;
        yield return new WaitForSeconds(bulletCooldownTime);
        isInCooldown = false;
    }

    protected void UpdateBulletsUI()
    {
        EventsManager.Instance.BroadcastUpdateBulletsCounter();
    }

    public int[] GetBulletsCounter()
    {
        int[] bulletsCounter = new int[2];
        bulletsCounter[0] = totalBullets;
        bulletsCounter[1] = bulletsLoaded;
        return bulletsCounter;
    }

    public void AddAmmo(int ammo)
    {
        totalBullets += ammo;
    }

}
