using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput)), RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // REFERENCE PROPERTIES
    public CharacterController Controller { get; private set; }

    // STATE MACHINE PROPERTIES
    public PlayerState CurrentState { get; private set; }
    public PlayerState PreviousState {  get; private set; }

    [field: SerializeField] public PlayerGroundState GroundState { get; private set; } = new PlayerGroundState();

    // MOVEMENT PROPERTIES
    public Vector3 Direction { get; set; }
    public Vector3 Velocity { get; set; }
    public float CurrentSpeed { get; set; }
    public float VerticalSpeed {  get; set; }

    // INPUT PROPERTIES
    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Jump {  get; private set; }
    public bool Melee {  get; private set; }
    public bool Shoot { get; private set; }
    public bool Interact { get; private set; }

    // Input reference
    private PlayerInput input;

    // Universal Movement Variables
    public float stickForce;
    public float gravity;
    public float turnSpeed;

    private void Start()
    {
        Controller = GetComponent<CharacterController>();

        input = GetComponent<PlayerInput>();

        input.onActionTriggered += OnAction;

        SetState(GroundState);
    }

    private void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.UpdateState(this);
            CurrentState.ChangeState(this);
        }

        ApplyMovement();
    }

    private void OnDisable()
    {
        input.onActionTriggered -= OnAction;
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Move": Move = context.ReadValue<Vector2>(); break;
            case "Look": Look = context.ReadValue<Vector2>(); break;
            case "Jump": Jump = context.ReadValue<float>() > 0.5f; break;
            case "Melee": Melee = context.ReadValue<float>() > 0.5f; break;
            case "Shoot": Shoot = context.ReadValue<float>() > 0.5f; break;
            case "Interact": Interact = context.ReadValue<float>() > 0.5f; break;
            case "Pause": break;
        }
    }

    public void SetState(PlayerState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.ExitState(this);

            if (CurrentState != newState)
            {
                PreviousState = CurrentState;
            }
        }

        CurrentState = newState;

        if (CurrentState != null)
        {
            CurrentState.StartState(this);
        }
    }

    private void ApplyMovement()
    {
        Vector3 velocity = CurrentSpeed * Direction;
        velocity.y = VerticalSpeed;

        Velocity = velocity;

        Controller.Move(Velocity * Time.deltaTime);
    }

    public void FaceDirection(Vector3 forward, float speed)
    {
        if (forward == Vector3.zero) return;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(forward), speed * Time.deltaTime);
    }
}
