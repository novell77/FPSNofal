using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private ZombieHealth health;

    public float chaseRange = 10f;
    public float attackRange = 2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = GetComponent<ZombieHealth>();
    }

    void Update()
    {
        if (health != null && health.IsDead) return;
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > chaseRange)
        {
            agent.isStopped = true;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Z_Idle"))
            {
                animator.Play("Z_Idle");
            }
        }
        else if (distance > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Z_Run_InPlace"))
            {
                animator.Play("Z_Run_InPlace");
            }
        }
        else
        {
            agent.isStopped = true;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Z_Attack"))
            {
                animator.Play("Z_Attack");
            }
        }

        // الدوران باتجاه اللاعب
        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f);
        }
    }
}
