using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    // Assign these in the inspector
    [field: SerializeField]
    public SpriteRenderer SR { get; private set; }
    [field: SerializeField]
    public Collider2D Col {get; private set; }
    [field: SerializeField]
    public Rigidbody2D RB { get; private set; }

    #region State Machine Variables

    public StateMachine<Player> StateMachine { get; private set; } = new StateMachine<Player>();

    [SerializeField] PlayerIdleSO playerIdleSOBase;
    [SerializeField] PlayerMoveSO playerMoveSOBase;

    public PlayerIdleSO PlayerIdleSOInstance { get; private set; }
    public PlayerMoveSO PlayerMoveSOInstance { get; private set; }

    public State<Player> IdleState { get; private set; }
    public State<Player> MoveState { get; private set; }

    #endregion

    #region Input Variables

    InputAction moveAction;

    #endregion

    #region Movement Variables

    public Vector2 moveWish;
    public Vector2 movement;
    public float movementSpeed = 5f;

    #endregion

    void Awake()
    {
        PlayerIdleSOInstance = Instantiate(playerIdleSOBase);
        PlayerMoveSOInstance = Instantiate(playerMoveSOBase);

        IdleState = new State<Player>(this, PlayerIdleSOInstance);
        MoveState = new State<Player>(this, PlayerMoveSOInstance);

        StateMachine.Initialize(IdleState);
    }

    void Start()
    {
        moveAction = InputManager.GetAction("Move");
        moveAction.performed += OnMoveAction;
        moveAction.canceled += OnMoveAction;
    }

    void OnDestroy()
    {
        moveAction.performed -= OnMoveAction;
        moveAction.canceled -= OnMoveAction;
    }

    void Update()
    {
        StateMachine.CurrentState.FrameUpdate();
    }

    void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #region Movement Methods

    public void SetMovement()
    {
        movement = moveWish;
    }

    public void Move()
    {
        RB.linearVelocity = movement * movementSpeed;
    }

    #endregion

    #region Input Methods

    void OnMoveAction(InputAction.CallbackContext context)
    {
        moveWish = context.ReadValue<Vector2>().normalized;
    }

    #endregion
}
