using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Walk / Run Speeds")]
    public float walkSpeed = 5f;
    public float runSpeed = 9f;

    [Header("Crouch")]
    public float standHeight = 2f;
    public float crouchHeight = 1f;
    [Range(0.1f, 1f)] public float crouchSpeedFactor = 0.5f;

    [Header("Jump / Gravity")]
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isCrouching;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        controller.height = standHeight;
    }

    private void Update()
    {
        // 1. «·√—÷ Ground Check
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        // 2. «‰»ÿ«Õ Toggle (C)
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
            controller.height = isCrouching ? crouchHeight : standHeight;
        }

        // 3. Ã—Ì Shift
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runSpeed : walkSpeed;
        if (isCrouching) speed *= crouchSpeedFactor;

        // 4. Õ—ﬂ… WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // 5. ﬁ›“ Space
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // 6. Ã«–»Ì… Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
