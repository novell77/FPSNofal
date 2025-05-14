using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public static KillCounter instance;

    public int killCount = 0;
    public int winKillCount = 100;
    public TextMeshProUGUI killText;

    void Awake()
    {
        instance = this;
        UpdateUI();
    }

    public void AddKill()
    {
        killCount++;
        UpdateUI();

        if (killCount >= winKillCount)
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
        // هنا تقدر توقف اللعبة أو تظهر شاشة فوز أو تنقل لمشهد جديد
    }
}
