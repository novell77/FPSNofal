using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public static ZombieSpawner instance;

    public GameObject zombiePrefab;
    public Transform[] spawnPoints;
    public float respawnDelay = 2f;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnNewZombie()
    {
        StartCoroutine(RespawnZombie());
    }

    IEnumerator RespawnZombie()
    {
        yield return new WaitForSeconds(respawnDelay);

        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(zombiePrefab, randomPoint.position, randomPoint.rotation);
    }
}
