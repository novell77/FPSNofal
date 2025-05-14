using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    public float walkRange = 10f;
    public float runRange = 6f;
    public float attackRange = 2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        animator.SetFloat("Distance", distance);

        if (distance > walkRange)
        {
            agent.isStopped = true;
            animator.Play("Z_Idle");
        }
        else if (distance > runRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.Play("Z_Walk1_InPlace");
        }
        else if (distance > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.Play("Z_Run_InPlace");
        }
        else
        {
            agent.isStopped = true;
            animator.Play("Z_Attack");
        }

        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f);
        }
    }
}
