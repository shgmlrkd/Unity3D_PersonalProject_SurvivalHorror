using UnityEngine;
using UnityEngine.Playables;

public class PlayerAnimation : MonoBehaviour
{
    private int RUN_ANIM_HASH = Animator.StringToHash("IsRun");
    private int MOVE_ANIM_HASH = Animator.StringToHash("IsMove");

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private PlayerInputHandler inputHandler;

    private void Awake()
    {
        if(animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if(inputHandler == null)
        {
            inputHandler = GetComponent<PlayerInputHandler>();
        }
    }

    private void OnEnable()
    {
        inputHandler.OnMove += SetMove;
        inputHandler.OnRun += SetRun;
    }

    private void OnDisable()
    {
        inputHandler.OnMove -= SetMove;
        inputHandler.OnRun -= SetRun;
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
}