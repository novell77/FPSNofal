using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public static ZombieSpawner instance;

    [Header("Spawn Settings")]
    [Tooltip("Prefab of the zombie to spawn")]
    public GameObject zombiePrefab;
    [Tooltip("Transforms where zombies will respawn")]
    public Transform[] spawnPoints;
    [Tooltip("Delay before respawning a new zombie")]
    public float respawnDelay = 2f;

    [Header("Win Condition (kills required)")]
    [Tooltip("Number of kills required to open the door")]
    public int killLimit = 2;

    [Header("Door Settings")]
    [Tooltip("Door that opens when player wins")]
    public Transform finalDoor;
    [Tooltip("How much to move the door when opening")]
    public Vector3 doorOpenOffset = new Vector3(0, 3, 0);
    [Tooltip("Speed at which the door opens")]
    public float doorOpenSpeed = 2f;

    private Vector3 doorClosedPos;
    private Vector3 doorOpenPos;
    private bool gameEnded = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }

        if (finalDoor != null)
        {
            doorClosedPos = finalDoor.localPosition;
            doorOpenPos = doorClosedPos + doorOpenOffset;
        }
    }

    void Update()
    {
        if (!gameEnded && KillCounter.instance != null)
        {
            int kc = KillCounter.instance.killCount;
            Debug.Log($"[Spawner] Current kills = {kc}, limit = {killLimit}");

            if (kc >= killLimit)
            {
                gameEnded = true;
                Debug.Log("[Spawner] Win reached! Opening final door.");
            }
        }

        if (gameEnded && finalDoor != null)
        {
            finalDoor.localPosition = Vector3.Lerp(
                finalDoor.localPosition,
                doorOpenPos,
                Time.deltaTime * doorOpenSpeed
            );
        }
    }

    public void SpawnNewZombie()
    {
        if (KillCounter.instance != null
            && KillCounter.instance.killCount >= killLimit)
            return;

        StartCoroutine(RespawnZombie());
    }

    private IEnumerator RespawnZombie()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (KillCounter.instance != null
            && KillCounter.instance.killCount >= killLimit)
            yield break;

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("[Spawner] No spawn points assigned!");
            yield break;
        }
        if (zombiePrefab == null)
        {
            Debug.LogWarning("[Spawner] No zombiePrefab assigned!");
            yield break;
        }

        var pt = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(zombiePrefab, pt.position, pt.rotation);
    }
}
