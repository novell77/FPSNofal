using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public static KillCounter instance;

    public int killCount = 0;
    public int winKillCount = 100;
    public TextMeshProUGUI killText;

    public Transform finalDoor;
    public Vector3 openOffset = new Vector3(0, 3f, 0); // مثال: الباب يرتفع للأعلى
    public float openSpeed = 2f;

    private bool hasWon = false;
    private Vector3 finalDoorClosedPos;
    private Vector3 finalDoorOpenPos;

    void Awake()
    {
        instance = this;
        finalDoorClosedPos = finalDoor.localPosition;
        finalDoorOpenPos = finalDoorClosedPos + openOffset;
        UpdateUI();
    }

    void Update()
    {
        if (hasWon && finalDoor != null)
        {
            finalDoor.localPosition = Vector3.Lerp(finalDoor.localPosition, finalDoorOpenPos, Time.deltaTime * openSpeed);
        }
    }

    public void AddKill()
    {
        killCount++;
        UpdateUI();

        if (killCount >= winKillCount && !hasWon)
        {
            Win();
        }
    }

    void UpdateUI()
    {
        if (killText != null)
        {
            killText.text = "Kills: " + killCount.ToString();
        }
    }

    void Win()
    {
        Debug.Log("You Win!");
        hasWon = true;
    }
}
