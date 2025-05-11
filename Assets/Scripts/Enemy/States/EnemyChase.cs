
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed = 2f;

    public void DoChase(Transform player)
    {
        transform.LookAt(player);
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
