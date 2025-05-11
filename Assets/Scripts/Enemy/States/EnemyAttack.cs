
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackCooldown = 1.5f;
    public int damage = 1;
    private float lastAttackTime;

    public void DoAttack(Transform player)
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }
}
