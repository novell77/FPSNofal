using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public static KillCounter instance;

    [Header("UI")]
    [Tooltip("Text element to display kills")]
    public TextMeshProUGUI killText;

    [Header("Win & Door")]
    [Tooltip("Door to open on win")]
    public Transform finalDoor;
    [Tooltip("How much to lift the door")]
    public Vector3 openOffset = new Vector3(0, 3f, 0);
    [Tooltip("Speed of door opening")]
    public float openSpeed = 2f;

    [Header("Win Target")]
    [Tooltip("Kills needed to win")]
    public int winKillCount = 2;

    [HideInInspector] public int killCount = 0;

    private bool hasWon = false;
    private Vector3 doorClosedPos;
    private Vector3 doorOpenPos;

    void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }

        doorClosedPos = finalDoor.localPosition;
        doorOpenPos = doorClosedPos + openOffset;
        UpdateUI();
    }

    void Update()
    {
        if (hasWon && finalDoor != null)
        {
            finalDoor.localPosition = Vector3.Lerp(
                finalDoor.localPosition,
                doorOpenPos,
                Time.deltaTime * openSpeed
            );
        }
    }

    public void AddKill()
    {
        killCount++;
        UpdateUI();

        if (!hasWon && killCount >= winKillCount)
        {
            hasWon = true;
            Debug.Log($"[KillCounter] Win reached at {killCount} kills");
        }
    }

    private void UpdateUI()
    {
        if (killText != null)
            killText.text = $"Kills: {killCount}";
    }
}
