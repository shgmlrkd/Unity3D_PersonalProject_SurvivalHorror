using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction runAction;
    private InputAction jumpAction;

    private Vector2 moveInput;

    private bool isMoving;
    private bool isRunning;

    public Vector2 MoveInput => moveInput;
    public bool IsMoving => isMoving;
    public bool IsRunning => isRunning;
    public bool CanJump => jumpAction.WasPressedThisFrame();

    public event Action<bool> OnMove;
    public event Action<bool> OnRun;

    private void Awake()
    {
        runAction = InputSystem.actions.FindAction("Sprint");
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Input System 액션 이벤트 구독
    private void OnEnable()
    {
        if (moveAction != null)
        { 
            moveAction.started += MoveChanged;
            moveAction.canceled += MoveChanged;
        }

        if (runAction != null)
        {
            runAction.started += RunChanged;
            runAction.canceled += RunChanged;
        }
    }

    // Input System 액션 이벤트 해제
    private void OnDisable()
    {
        if (moveAction != null)
        {
            moveAction.started -= MoveChanged;
            moveAction.canceled -= MoveChanged;
        }

        if (runAction != null)
        {
            runAction.started -= RunChanged;
            runAction.canceled -= RunChanged;
        }
    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }

    // WASD 입력 시 걷기 애니메이션 이벤트
    private void MoveChanged(InputAction.CallbackContext context)
    {
        bool nextIsMoving = !context.canceled;

        if (isMoving == nextIsMoving)
            return;

        isMoving = nextIsMoving;

        OnMove?.Invoke(isMoving);
    }
    
    // Shift 입력 시 뛰기 애니메이션 이벤트
    private void RunChanged(InputAction.CallbackContext context)
    {
        bool shouldRun = isMoving && runAction.IsPressed();

        if (isRunning == shouldRun)
            return;

        isRunning = shouldRun;

        OnRun?.Invoke(isRunning);
    }
}