using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public int damageAmount = 1;
    private bool hasAttacked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasAttacked)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                hasAttacked = true;
                Invoke("ResetAttack", 1f);
            }
        }
    }

    private void ResetAttack()
    {
        hasAttacked = false;
    }
}
