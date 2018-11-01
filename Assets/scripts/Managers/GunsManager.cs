using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsManager : MonoBehaviour {

    public Transform playerGunPositionHolder;

    public List<GameObject> guns;

    public List<FollowCursorGun> gunsScripts;

    public enum GunSelected { Ninemm };

    private float fixYGunPosition;

    public static GameObject currentGun;

    // Use this for initialization
    void Start () {

        foreach (Transform gun in transform) {
            guns.Add(gun.gameObject);
            gunsScripts.Add(gun.gameObject.GetComponent<FollowCursorGun>());
        }

        fixYGunPosition = gunsScripts[(int)GunSelected.Ninemm].positionXFix;

        Vector3 fixedGunPosition = new Vector3(
            playerGunPositionHolder.position.x + fixYGunPosition,
            playerGunPositionHolder.position.y,
            0);
        //Crear más armas para colocar
        currentGun = Instantiate(guns[(int)GunSelected.Ninemm], fixedGunPosition, playerGunPositionHolder.rotation);
        
        currentGun.transform.parent = playerGunPositionHolder;
        currentGun.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
