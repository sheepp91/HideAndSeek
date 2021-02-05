using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    private const float STAND_SCALE = 1.0f;
    private const float CROUCH_SCALE = 0.5f;

    public CharacterController controller;
    public Transform cam;
    public float walkSpeed = 10;
    public float sprintSpeed = 15;

    public Transform playerModel;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float gravity = -9.81f;
    private Vector3 velocity;
    bool isGrounded;
    public float jumpHeight = 3f;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    void Update()
    {
        CheckGrounded();
        Move();
        Jump();
        Crouch();
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetAxis("Sprint") > 0.5;
            float speed = isSprinting ? sprintSpeed : walkSpeed;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Crouch()
    {
        bool isCrouching = Input.GetKey(KeyCode.LeftControl) || Input.GetButton("Crouch");
        if (isCrouching)
        {
            Vector3 scale = playerModel.localScale;
            scale.y = CROUCH_SCALE;
            playerModel.localScale = scale;
        }
        else
        {
            Vector3 scale = playerModel.localScale;
            scale.y = STAND_SCALE;
            playerModel.localScale = scale;
        }
    }
}
