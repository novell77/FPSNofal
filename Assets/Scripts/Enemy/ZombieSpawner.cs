using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public static ZombieSpawner instance;

    public GameObject zombiePrefab;
    public Transform[] spawnPoints;
    public float respawnDelay = 2f;

    [Header("Win Condition")]
    public int killLimit = 10;
    public Transform finalDoor;
    public Vector3 doorOpenOffset = new Vector3(0, 3, 0);
    public float doorOpenSpeed = 2f;

    private Vector3 doorClosedPos;
    private Vector3 doorOpenPos;
    private bool gameEnded = false;

    private void Awake()
    {
        instance = this;

        if (finalDoor != null)
        {
            doorClosedPos = finalDoor.localPosition;
            doorOpenPos = doorClosedPos + doorOpenOffset;
        }
    }

    private void Update()
    {
        if (!gameEnded && KillCounter.instance != null && KillCounter.instance.killCount >= killLimit)
        {
            gameEnded = true;
            Debug.Log("وصل حد الفوز! فتح الباب النهائي");
        }

        if (gameEnded && finalDoor != null)
        {
            finalDoor.localPosition = Vector3.Lerp(finalDoor.localPosition, doorOpenPos, Time.deltaTime * doorOpenSpeed);
        }
    }

    public void SpawnNewZombie()
    {
        if (KillCounter.instance != null && KillCounter.instance.killCount >= killLimit)
            return;

        StartCoroutine(RespawnZombie());
    }

    IEnumerator RespawnZombie()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (KillCounter.instance != null && KillCounter.instance.killCount >= killLimit)
            yield break;

        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(zombiePrefab, randomPoint.position, randomPoint.rotation);
    }
}
