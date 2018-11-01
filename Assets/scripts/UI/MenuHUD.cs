using UnityEngine;
using UnityEngine.UI;

public class MenuHUD : MonoBehaviour {

    public Text bulletText;

    private string bulletString;
    public GunBehaviour gun;
    // Use this for initialization
    void Start () {
        EventsManager.Instance.UpdateBulletsCounterUIListener += UpdateBulletsCounterUI;
        EventsManager.Instance.SwitchGunListener += PlayerSwitchedGun;

        bulletString = bulletText.text + " ";

        UpdateBulletsCounterUI();
    }

    void OnDestroy() {
        EventsManager.Instance.UpdateBulletsCounterUIListener -= UpdateBulletsCounterUI;
        EventsManager.Instance.SwitchGunListener -= PlayerSwitchedGun;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void UpdateBulletsCounterUI() {

        if (gun == null) {
            return;
        }

        int[] bulletsCounter = gun.GetBulletsCounter();
        //TODO definir un enum global para este array
        int totalBullets = bulletsCounter[0];
        int bulletsLoaded = bulletsCounter[1];


        string concatenateBulletString = string.Concat(
            bulletString,
            bulletsLoaded,
            "/",
            totalBullets);

        bulletText.text = concatenateBulletString;
    }

    private void PlayerSwitchedGun(GunBehaviour gunBehaviour) {
        gun = gunBehaviour;
    }
}
