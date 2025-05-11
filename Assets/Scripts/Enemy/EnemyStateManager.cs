
using UnityEngine;

[RequireComponent(typeof(EnemyIdle))]
[RequireComponent(typeof(EnemyChase))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(EnemyDead))]
public class EnemyStateManager : MonoBehaviour
{
    public EnemyState currentState = EnemyState.Idle;
    public Transform player;

    private EnemyIdle idle;
    private EnemyChase chase;
    private EnemyAttack attack;
    private EnemyDead dead;

    void Start()
    {
        idle = GetComponent<EnemyIdle>();
        chase = GetComponent<EnemyChase>();
        attack = GetComponent<EnemyAttack>();
        dead = GetComponent<EnemyDead>();
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                idle.DoIdle();
                break;
            case EnemyState.Chase:
                chase.DoChase(player);
                break;
            case EnemyState.Attack:
                attack.DoAttack(player);
                break;
            case EnemyState.Dead:
                dead.DoDead();
                break;
        }
    }

    public void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }
}
