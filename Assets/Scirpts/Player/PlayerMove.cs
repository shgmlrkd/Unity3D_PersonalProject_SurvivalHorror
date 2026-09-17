using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float gravity = -9.81f;

    [SerializeField]
    private float walkSpeed = 4.0f;

    [SerializeField]
    private float runSpeed = 7.0f;

    private float jumpPower = 5.0f;

    [SerializeField]
    private PlayerAnimation playerAnim;

    [SerializeField]
    private PlayerInputHandler playerInput;

    [SerializeField]
    private CharacterController characterController;

    public event Action OnJump;

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
        Jump();

        Gravity();

        Move();

        UpdateJumpVelocityAnimation();
    }

    // 중력 적용
    private void Gravity()
    {
        if (characterController.isGrounded && verticalVelocity < 0.0f)
        {
            verticalVelocity = 0.0f;
        }

        verticalVelocity += gravity * Time.deltaTime;
    }

    // 플레이어가 보는 방향 기준 움직임
    private void Move()
    {
        Vector2 moveDir = playerInput.MoveInput;

        Vector3 moveDirection = transform.forward * moveDir.y + transform.right * moveDir.x;

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            moveDirection.Normalize();
        }

        float moveSpeed = playerInput.IsRunning ? runSpeed : walkSpeed;

        Vector3 horizontalDirection = moveDirection * moveSpeed;
        Vector3 verticalDirection = Vector3.up * verticalVelocity;

        Vector3 finalDirection = horizontalDirection + verticalDirection;

        characterController.Move(finalDirection * Time.deltaTime);
    }

    // 플레이어 실제 점프
    private void Jump()
    {
        if (!characterController.isGrounded)
            return;

        if (!playerInput.CanJump)
            return;

        verticalVelocity = jumpPower;

        OnJump?.Invoke();
    }

    // 플레이어 점프 애니메이션 (가속도에 따른 애니메이션 변화)
    private void UpdateJumpVelocityAnimation()
    {
        float animVelocityY = characterController.isGrounded ? 0.0f : verticalVelocity;

        playerAnim.SetJumpVelocity(animVelocityY);
    }
}