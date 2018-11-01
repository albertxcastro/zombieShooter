using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawner : MonoBehaviour {

    //publics
    public GameObject normalZombie;
    public GameObject bigMuncherZombie;

    public float normalZombieSpawnTime = 1f;
    public float bigMuncherSpawnTime = 1f;

    public bool keepSpawningZombies = true;
    public bool keepSpawningNormalZombie = true;
    public bool keepSpawningBigMuncher = true;

    public bool isSpawningNormalZombie = false;
    public bool isSpawningBigMuncher = false;
    public bool isGameOver = false;

	// Use this for initialization
	void Start () {
        EventsManager.Instance.GameOverListener += GameOver;
    }

    void OnDestroy()
    {
        EventsManager.Instance.GameOverListener -= GameOver;
    }
	
	// Update is called once per frame
	void Update () {

        if (!keepSpawningZombies)
        {
            return;
        }

        if (keepSpawningBigMuncher)
        {
            if (!isSpawningBigMuncher)
            {
                isSpawningBigMuncher = true;
                StartCoroutine(SpawnZombieTimer(bigMuncherZombie, bigMuncherSpawnTime));
            }
        }

        if (keepSpawningNormalZombie) {
            if (!isSpawningNormalZombie)
            {
                isSpawningNormalZombie = true;
                StartCoroutine(SpawnZombieTimer(normalZombie, normalZombieSpawnTime));
            }
        }
    }

    IEnumerator SpawnZombieTimer(GameObject zombieToSpawn, float spawnTime) {
        
        yield return new WaitForSecondsRealtime(spawnTime);
        if (!isGameOver)
        {
            SpawnZombie(zombieToSpawn);
        }
    }

    private void SpawnZombie(GameObject zombieToSpawn) {
        var newZombie = Instantiate(zombieToSpawn, transform.position, transform.rotation);
        newZombie.SetActive(true);

        var zombieScript = newZombie.GetComponent<ZombieScript>();

        switch (zombieScript.ZombieType)
        {
            case ZombieScript.ZombieTypeEnum.normalZombie:
                newZombie.transform.localScale += new Vector3(Random.Range(-0.1f, 0.2f), Random.Range(-0.15f, 0.25f), 0);
                isSpawningNormalZombie = false;
                break;

            case ZombieScript.ZombieTypeEnum.bigMuncherZombie:
                isSpawningBigMuncher = false;
                break;

            default:
                Debug.Log("Compadre, te faltó definir un zombie en el spawner");
                break;
        }
    }

    private void GameOver() {
        isGameOver = true;
        keepSpawningZombies = false;
    }
}
