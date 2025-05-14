
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    private float uiHealthDisplay;

    void Start()
    {
        currentHealth = maxHealth;
        uiHealthDisplay = maxHealth;
        GameUIManager.instance.UpdatePlayerHealth(currentHealth, maxHealth);
    }

    void Update()
    {
        float targetValue = (float)currentHealth / maxHealth;
        uiHealthDisplay = Mathf.Lerp(uiHealthDisplay, targetValue, Time.deltaTime * 8f);
        GameUIManager.instance.UpdatePlayerHealth(Mathf.RoundToInt(uiHealthDisplay * maxHealth), maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
