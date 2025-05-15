using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damageAmount = 1;      // كم يخصم من الصحة
    public float attackCooldown = 1f; // ثواني بين كل ضربة
    private float nextAttackTime = 0f;

    // هذا يُستدعى كل فريم طالما اللاعب داخل التريغر
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= nextAttackTime)
        {
            var ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damageAmount);
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }
}
