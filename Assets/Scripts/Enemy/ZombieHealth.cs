using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private ZombieAI ai;
    private Animator animator;

    public bool IsDead { get; private set; }

    void Start()
    {
        currentHealth = maxHealth;
        ai = GetComponent<ZombieAI>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        IsDead = true;
        ai.enabled = false;
        animator.Play("Z_Death");
        Destroy(gameObject, 5f);
    }
}
