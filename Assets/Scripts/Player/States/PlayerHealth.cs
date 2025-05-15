using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    int currentHealth;
    float uiDisp;

    void Start()
    {
        currentHealth = maxHealth;
        uiDisp = maxHealth;
        GameUIManager.instance.UpdatePlayerHealth(currentHealth, maxHealth);
    }

    void Update()
    {
        float t = (float)currentHealth / maxHealth;
        uiDisp = Mathf.Lerp(uiDisp, t, Time.deltaTime * 8f);
        GameUIManager.instance.UpdatePlayerHealth(
            Mathf.RoundToInt(uiDisp * maxHealth),
            maxHealth
        );
    }

    public void TakeDamage(int amt)
    {
        currentHealth = Mathf.Max(0, currentHealth - amt);
        if (currentHealth <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int GetCurrentHealth() => currentHealth;
}
