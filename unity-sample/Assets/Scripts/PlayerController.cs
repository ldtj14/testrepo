using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 velocity;
    private bool jumpQueued;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpQueued = true;
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Hook this up to a weapon or interaction system.
            Debug.Log("Fire action triggered");
        }
    }

    private void Update()
    {
        ApplyMovement();
        ApplyGravity();
    }

    private void ApplyMovement()
    {
        Vector3 inputDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        if (inputDirection.sqrMagnitude > 0.001f)
        {
            controller.Move(transform.TransformDirection(inputDirection) * moveSpeed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(transform.TransformDirection(inputDirection));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded)
        {
            if (velocity.y < 0f)
            {
                velocity.y = -2f; // small downward force to keep grounded
            }

            if (jumpQueued)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jumpQueued = false;
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
