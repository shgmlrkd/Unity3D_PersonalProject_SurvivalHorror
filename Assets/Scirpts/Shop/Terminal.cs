using System;
using UnityEngine;

public class Terminal : MonoBehaviour, IInteractable, IInteractText
{
    [SerializeField]
    private Collider bodyCollider;

    [SerializeField]
    private Collider screenCollider;

    [SerializeField]
    private Transform viewPoint;

    public float InteractionDuration => 0.0f;

    public Transform ViewPoint => viewPoint;

    public bool IsUsing { get; private set; }

    public string InteractionText => "터미널 사용 : [E]";

    public event Action OnEntered;

    private void Awake()
    {
        screenCollider.enabled = false;
    }

    public void Interact()
    {
        if (IsUsing)
        {
            return;
        }

        IsUsing = true;

        bodyCollider.enabled = false;
        screenCollider.enabled = true;

        OnEntered?.Invoke();
    }

    public void Exit()
    {
        if (!IsUsing)
        {
            return;
        }

        IsUsing = false;

        screenCollider.enabled = false;
        bodyCollider.enabled = true;
    }
}