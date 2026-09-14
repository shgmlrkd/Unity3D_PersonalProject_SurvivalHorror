using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float gravity = -9.81f;

    [SerializeField]
    private float walkSpeed = 4.0f;

    [SerializeField]
    private float runSpeed = 7.0f;

    [SerializeField]
    private PlayerAnimation playerAnim;

    [SerializeField]
    private PlayerInputHandler playerInput;

    [SerializeField]
    private CharacterController characterController;

    private float verticalVelocity;

    private void Awake()
    {
        if(playerAnim == null)
        {
            playerAnim = GetComponent<PlayerAnimation>();
        }    

        if(playerInput == null)
        {
            playerInput = GetComponent<PlayerInputHandler>();
        }

        if(characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
    }

    private void Update()
    {
        Gravity();

        Move();
    }

    private void Gravity()
    {
        if (characterController.isGrounded && verticalVelocity < 0.0f)
        {
            verticalVelocity = -2.0f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    // 플레이어가 보는 방향 기준 움직임
    private void Move()
    {
        Vector2 moveDir = playerInput.MoveInput;

        if (moveDir.sqrMagnitude < 0.01f) return;

        Vector3 moveDirection = transform.forward * moveDir.y + transform.right * moveDir.x;
        moveDirection.Normalize();

        float moveSpeed = playerInput.IsRunning ? runSpeed : walkSpeed;

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}