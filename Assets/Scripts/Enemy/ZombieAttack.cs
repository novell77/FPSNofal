using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damageAmount = 1;
    public float attackRate = 1f;    // ثانية بين كل ضربة
    private float nextAttackTime = 0f;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= nextAttackTime)
        {
            var ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damageAmount);
                Debug.Log("Player hit by zombie! Current Health = " + ph.GetCurrentHealth());
                nextAttackTime = Time.time + attackRate;
            }
        }
    }
}
