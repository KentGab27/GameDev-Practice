using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;

    [SerializeField] PlayerInputActions inputActions;
    [SerializeField] float moveInput;

    private InputAction moveAction;
    private Rigidbody2D rb;
    private Animator animator;

    static readonly int isMovingHash = Animator.StringToHash("isMoving");

    void OnEnable()
    {
        inputActions.Player.Enable();
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<float>();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        inputActions = new PlayerInputActions();
        moveAction = inputActions.Player.Move;
    }
    
    void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveInput * moveSpeed;

        rb.linearVelocity = velocity;

        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        animator.SetBool(isMovingHash, moveInput != 0f);

        if(moveInput != 0f)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(moveInput) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}
