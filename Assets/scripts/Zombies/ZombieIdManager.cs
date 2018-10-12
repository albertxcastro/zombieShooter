using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdManager : MonoBehaviour {

    public static int zombieID = 0;

    public static int getZombieID() {
        return zombieID++;
    }
	
}
