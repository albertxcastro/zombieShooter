using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour {

    public GameObject smallAmmo;

    public float spawnItemsLuck = 1f;
    public float spawnItemsLuckMultiplier = 1f;
    public float normalZombieSpawnRatio = 0.2f;
    public float bigMuncherSpawnRatio = 0.2f;
    public int maximumAmmoOnScene = 3;
    public static int ammoOnScene = 0;

    public float randomGenerated = 0;

    public enum ItemType { smallAmmoItem };

	// Use this for initialization
	void Start () {
        EventsManager.Instance.ZombieDiedListener += SpawnItemZombieDied;
    }

    private void SpawnItemZombieDied(ZombieScript.ZombieTypeEnum zombieType, Transform zombiePosition)
    {
        bool didItemSpawn;
        ItemType itemType;
        randomGenerated = Random.Range(0f, 1f);


        switch (zombieType)
        {
            case ZombieScript.ZombieTypeEnum.normalZombie:
                
                didItemSpawn =  randomGenerated < spawnItemsLuck * normalZombieSpawnRatio * spawnItemsLuckMultiplier;
                itemType = ItemType.smallAmmoItem;
                break;

            default:
                didItemSpawn = randomGenerated < spawnItemsLuck * bigMuncherSpawnRatio * spawnItemsLuckMultiplier; ;
                itemType = ItemType.smallAmmoItem;
                break;
        }

        if (didItemSpawn)
        {
            if (ammoOnScene < maximumAmmoOnScene)
            {
                SpawnItem(itemType, zombiePosition);
                spawnItemsLuck = 1;
                ammoOnScene++;
            }
            
        }
        else
        {
            spawnItemsLuck++;
        }
    }

    private void SpawnItem(ItemType itemType, Transform zombiePosition)
    {
        GameObject item;

        switch (itemType)
        {
            case ItemType.smallAmmoItem:
                item = smallAmmo;
                break;

            default:
                item = smallAmmo;
                break;
        }

        Vector3 itemPosiion = new Vector3(zombiePosition.position.x, zombiePosition.position.y, -1);
        var newItem = Instantiate(item, itemPosiion, Quaternion.Euler(Vector3.zero) );
        newItem.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
