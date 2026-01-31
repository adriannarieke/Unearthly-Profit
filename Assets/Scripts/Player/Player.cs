using System.Collections.Generic;
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
    [SerializeField] Renderer visionCone;
    [SerializeField] float visionConeRotationLerpSpeed = 15f;
    float targetVisionConeAngle;

    #region State Machine Variables

    public StateMachine<Player> StateMachine { get; private set; } = new StateMachine<Player>();

    [SerializeField] PlayerIdleSO playerIdleSOBase;
    [SerializeField] PlayerMoveSO playerMoveSOBase;
    [SerializeField] PlayerMineSO playerMineSOBase;

    public PlayerIdleSO PlayerIdleSOInstance { get; private set; }
    public PlayerMoveSO PlayerMoveSOInstance { get; private set; }
    public PlayerMineSO PlayerMineSOInstance { get; private set; }

    public State<Player> IdleState { get; private set; }
    public State<Player> MoveState { get; private set; }
    public State<Player> MineState { get; private set; }

    #endregion

    #region Movement Variables

    public Vector2 moveWish;
    public Vector2 movement;
    public float movementSpeed = 5f;

    #endregion

    #region Input Variables

    InputAction moveAction;
    InputAction interactAction;

    #endregion

    readonly List<Crystal> crystalsInRange = new List<Crystal>();
    Crystal closestCrystal;

    void Awake()
    {
        PlayerIdleSOInstance = Instantiate(playerIdleSOBase);
        PlayerMoveSOInstance = Instantiate(playerMoveSOBase);
        PlayerMineSOInstance = Instantiate(playerMineSOBase);

        IdleState = new State<Player>(this, PlayerIdleSOInstance);
        MoveState = new State<Player>(this, PlayerMoveSOInstance);
        MineState = new State<Player>(this, PlayerMineSOInstance);

        StateMachine.Initialize(IdleState);
    }

    void Start()
    {
        moveAction = InputManager.GetAction("Move");
        moveAction.performed += OnMoveAction;
        moveAction.canceled += OnMoveAction;

        interactAction = InputManager.GetAction("Interact");
        interactAction.performed += OnInteractAction;
        interactAction.canceled += OnInteractAction;

        //visionCone.material.mainTexture = PlayerVisionCamera.Cam.targetTexture;
    }

    void OnDestroy()
    {
        moveAction.performed -= OnMoveAction;
        moveAction.canceled -= OnMoveAction;
        interactAction.performed -= OnInteractAction;
        interactAction.canceled -= OnInteractAction;
    }

    void Update()
    {
        StateMachine.CurrentState.FrameUpdate();

        if (movement != Vector2.zero)
        {
            visionCone.transform.rotation = Quaternion.Lerp(visionCone.transform.rotation, Quaternion.Euler(0f, 0f, targetVisionConeAngle), visionConeRotationLerpSpeed * Time.deltaTime);
        }
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
        if (movement != Vector2.zero)
        {
            targetVisionConeAngle = Vector3.Angle(Vector3.up, movement);
            if (SR.flipX)
            {
                targetVisionConeAngle *= -1f;
            }
        }
    }

    #endregion

    #region Collision/Trigger Methods

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Crystal crystal))
        {
            crystalsInRange.Add(crystal);
            CalculateClosestCrystal();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Crystal crystal))
        {
            crystalsInRange.Remove(crystal);
            CalculateClosestCrystal();
        }
    }

    #endregion

    #region Input Methods

    void OnMoveAction(InputAction.CallbackContext context)
    {
        moveWish = context.ReadValue<Vector2>().normalized;
    }

    void OnInteractAction(InputAction.CallbackContext context)
    {
        if (context.performed && closestCrystal != null)
        {
            PlayerMineSOInstance.targetCrystal = closestCrystal;
            movement = Vector2.zero;
            Move();
            StateMachine.ChangeState(MineState);
        }
        else
        {
            SetMovement();
            Move();
            StateMachine.ChangeState(movement == Vector2.zero ? IdleState : MoveState);
        }
    }

    #endregion

    void CalculateClosestCrystal()
    {
        if (crystalsInRange.Count == 0)
        {
            closestCrystal = null;
            return;
        }

        int closestCrystalIndex = 0;
        float dstToClosestCrystal = Vector2.Distance(transform.position, crystalsInRange[0].transform.position);
        for (int i = 1; i < crystalsInRange.Count; i++)
        {
            Crystal currentCrystal = crystalsInRange[i];

            float dstToCrystal = Vector2.Distance(transform.position, currentCrystal.transform.position);
            if (dstToCrystal < dstToClosestCrystal)
            {
                dstToClosestCrystal = dstToCrystal;
                closestCrystalIndex = i;
            }
        }
        closestCrystal = crystalsInRange[closestCrystalIndex];
    }
}
