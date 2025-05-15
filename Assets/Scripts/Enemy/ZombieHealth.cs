using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;
    private bool isDead = false;

    [Header("UI Smoothing")]
    private float uiHealthDisplay = 1f;

    private ZombieAI ai;
    private Animator animator;

    public GameObject bloodEffect;
    public Transform bloodSpawnPoint;

    void Start()
    {
        currentHealth = maxHealth;
        uiHealthDisplay = 1f;
        ai = GetComponent<ZombieAI>();
        animator = GetComponent<Animator>();
        GameUIManager.instance.UpdateZombieHealth(currentHealth, maxHealth);
    }

    void Update()
    {
        float targetValue = (float)currentHealth / maxHealth;
        uiHealthDisplay = Mathf.Lerp(uiHealthDisplay, targetValue, Time.deltaTime * 8f);
        GameUIManager.instance.UpdateZombieHealth(
            Mathf.RoundToInt(uiHealthDisplay * maxHealth),
            maxHealth
        );
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        ShowBloodEffect();

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        if (ai != null) ai.enabled = false;
        if (animator != null) animator.Play("Z_Death");
    }

    void ShowBloodEffect()
    {
        if (bloodEffect != null && bloodSpawnPoint != null)
            Instantiate(bloodEffect, bloodSpawnPoint.position, bloodSpawnPoint.rotation);
    }

    /// <summary>
    /// Indicates whether this zombie has died.
    /// </summary>
    public bool IsDead => isDead;
}
