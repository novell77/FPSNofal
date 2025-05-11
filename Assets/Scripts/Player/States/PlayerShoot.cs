
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float range = 50f;
    public int damage = 1;
    public Camera fpsCam;

    public void HandleShoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = fpsCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            if (Physics.Raycast(ray, out RaycastHit hit, range))
            {
                if (hit.collider.TryGetComponent(out EnemyStateManager enemy))
                {
                    enemy.ChangeState(EnemyState.Dead);
                }
            }
        }
    }
}
