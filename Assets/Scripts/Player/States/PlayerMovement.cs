using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float jumpHeight = 3f;
    public float gravity = -2f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private AudioManager audioManager;

    [System.Obsolete]
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + 0.1f);

        if (isGrounded && velocity.y < 0f)
            velocity.y = -1f;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runSpeed : walkSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            audioManager.PlayJump();
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        HandleFootsteps(move, isRunning);
    }

    private void HandleFootsteps(Vector3 move, bool isRunning)
    {
        bool isMoving = move.magnitude > 0.1f && isGrounded;

        if (isMoving)
        {
            if (isRunning)
                audioManager.PlayRun();
            else
                audioManager.PlayWalk();
        }
        else
        {
            audioManager.StopWalkRun();
        }
    }
}
