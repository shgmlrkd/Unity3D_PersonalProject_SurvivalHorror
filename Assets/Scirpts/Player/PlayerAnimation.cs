using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private int RUN_ANIM_HASH = Animator.StringToHash("IsRun");
    private int MOVE_ANIM_HASH = Animator.StringToHash("IsMove");
    private int JUMP_TRIGGER_ANIM_HASH = Animator.StringToHash("Jump");
    private int JUMP_VELOCITY_ANIM_HASH = Animator.StringToHash("VelocityY");

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private PlayerMove playerMove;

    [SerializeField]
    private PlayerInputHandler inputHandler;

    private void Awake()
    {
        if(animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if(playerMove == null)
        {
            playerMove = GetComponent<PlayerMove>();
        }

        if(inputHandler == null)
        {
            inputHandler = GetComponent<PlayerInputHandler>();
        }
    }

    private void OnEnable()
    {
        if (inputHandler != null)
        {
            inputHandler.OnMove += SetMove;
            inputHandler.OnRun += SetRun;
        }

        if (playerMove != null)
        {
            playerMove.OnJump += SetJump;
        }
    }

    private void OnDisable()
    {
        if (inputHandler != null)
        {
            inputHandler.OnMove -= SetMove;
            inputHandler.OnRun -= SetRun;
        }

        if (playerMove != null)
        {
            playerMove.OnJump -= SetJump;
        }
    }

    // 대기 또는 움직임 애니메이션
    public void SetMove(bool isMove)
    {
        animator.SetBool(MOVE_ANIM_HASH, isMove);
    }

    // 움직일때 걷는지 뛰는지 애니메이션
    public void SetRun(bool isRun)
    { 
        animator.SetBool(RUN_ANIM_HASH, isRun);
    }

    // 점프 애니메이션 트리거 발동
    private void SetJump()
    {
        animator.SetTrigger(JUMP_TRIGGER_ANIM_HASH);
    }

    // 점프 가속도에 따른 변화 애니메이션
    public void SetJumpVelocity(float verticalVelocity)
    {
        animator.SetFloat(JUMP_VELOCITY_ANIM_HASH, verticalVelocity);
    }
}