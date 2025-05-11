
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerShoot), typeof(PlayerHealth))]
public class PlayerManager : MonoBehaviour
{
    void Update()
    {
        GetComponent<PlayerMovement>().HandleMovement();
        GetComponent<PlayerShoot>().HandleShoot();
    }
}
