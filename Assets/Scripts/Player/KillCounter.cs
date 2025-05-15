using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public static KillCounter instance;

    [Header("UI")]
    [Tooltip("TextMeshProUGUI element to show the current kill count")]
    public TextMeshProUGUI killText;

    [Header("Win Condition")]
    [Tooltip("Number of kills required to open the door")]
    public int winKillCount = 2;

    [Header("Door Settings")]
    [Tooltip("The door Transform that will open when the player wins")]
    public Transform finalDoor;
    [Tooltip("Local offset to apply when opening the door")]
    public Vector3 openOffset = new Vector3(0, 3f, 0);
    [Tooltip("Speed at which the door opens")]
    public float openSpeed = 2f;

    [HideInInspector]
    public int killCount = 0;

    private bool doorOpening = false;
    private Vector3 doorClosedPos;
    private Vector3 doorOpenPos;

    void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
    }

    void Start()
    {
        // cache door positions
        if (finalDoor != null)
        {
            doorClosedPos = finalDoor.localPosition;
            doorOpenPos = doorClosedPos + openOffset;
        }
        UpdateUI();
    }

    void Update()
    {
        // if we've reached the win condition, animate the door
        if (doorOpening && finalDoor != null)
        {
            finalDoor.localPosition = Vector3.Lerp(
                finalDoor.localPosition,
                doorOpenPos,
                Time.deltaTime * openSpeed
            );
        }
    }

    /// <summary>
    /// Call this whenever a zombie dies
    /// </summary>
    public void AddKill()
    {
        killCount++;
        UpdateUI();

        // once we hit the required kill count, start opening the door
        if (!doorOpening && killCount >= winKillCount)
        {
            doorOpening = true;
            Debug.Log($"KillCounter: reached {killCount} kills, opening door");
        }
    }

    private void UpdateUI()
    {
        if (killText != null)
        {
            killText.text = $"Kills: {killCount}";
        }
    }
}
