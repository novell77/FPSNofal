using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private MouseLook mouseLook;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        mouseLook = GetComponentInChildren<MouseLook>();
    }
}
