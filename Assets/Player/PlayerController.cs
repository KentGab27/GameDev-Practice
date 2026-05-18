using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;
    private PlayerInputActions inputActions;
    private InputAction moveAction;
    private float moveInput;


    private void OnEnable()
    {
        inputActions.Player.Enable();
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<float>();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        inputActions = new PlayerInputActions();
        moveAction = inputActions.Player.Move;
    }
    
    private void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveInput * moveSpeed;

        rb.linearVelocity = velocity;
    }
}
