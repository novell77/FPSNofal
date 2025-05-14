using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private ZombieAI ai;
    private Animator animator;
    private bool isDead = false;

    public GameObject bloodEffect;
    public Transform bloodSpawnPoint;

    private float uiHealthDisplay = 1f;

    void Start()
    {
        currentHealth = maxHealth;
        ai = GetComponent<ZombieAI>();
        animator = GetComponent<Animator>();
        GameUIManager.instance.UpdateZombieHealth(currentHealth, maxHealth);
    }

    void Update()
    {
        float targetValue = (float)currentHealth / maxHealth;
        uiHealthDisplay = Mathf.Lerp(uiHealthDisplay, targetValue, Time.deltaTime * 8f);
        GameUIManager.instance.UpdateZombieHealth(Mathf.RoundToInt(uiHealthDisplay * maxHealth), maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        ShowBloodEffect();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (ai != null)
        {
            ai.enabled = false;
        }

        animator.Play("Z_Death");
    }

    void ShowBloodEffect()
    {
        if (bloodEffect != null && bloodSpawnPoint != null)
        {
            Instantiate(bloodEffect, bloodSpawnPoint.position, bloodSpawnPoint.rotation);
        }
    }

    public bool IsDead
    {
        get { return isDead; }
    }
}
