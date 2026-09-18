using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private LayerMask interactLayer;

    [SerializeField]
    private float interactDistance = 3.0f;

    private Transform camTransform;
    private Collider currentCollider;
    private IInteractable currentInteractable;

    private float interactTimer;
    private bool isInteractComplete;

    public event Action<IInteractable> OnInteractableChanged;
    public event Action OnInteractionStarted;

    private void Awake()
    {
        camTransform = Camera.main.transform;
    }
    private void OnDisable()
    {
        ClearInteractable();
    }

    private void Update()
    {
        CheckInteractable();
        HandleInteract();
    }

    private void CheckInteractable()
    {
        if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, interactDistance, interactLayer))
        {
            // 같은 Collider를 계속 바라보고 있으면 다시 탐색하지 않음
            if (currentCollider == hit.collider)
            {
                return;
            }

            currentCollider = hit.collider;
            currentInteractable = hit.collider.GetComponentInParent<IInteractable>();

            ResetInteract();

            OnInteractableChanged?.Invoke(currentInteractable);

            return;
        }

        if (currentCollider != null)
        {
            currentCollider = null;
            currentInteractable = null;

            ResetInteract();

            OnInteractableChanged?.Invoke(null);
        }
    }

    private void HandleInteract()
    {
        if (currentInteractable == null)
        {
            return;
        }

        // 즉시 상호작용
        if (currentInteractable.InteractionDuration <= 0.0f)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                StartInteraction();
            }

            return;
        }

        // 일정 시간 동안 E 키를 누르는 상호작용
        if (Keyboard.current.eKey.isPressed)
        {
            interactTimer += Time.deltaTime;

            if (!isInteractComplete && interactTimer >= currentInteractable.InteractionDuration)
            {
                StartInteraction();

                isInteractComplete = true;
            }
        }

        if (Keyboard.current.eKey.wasReleasedThisFrame)
        {
            ResetInteract();
        }
    }

    private void ClearInteractable()
    {
        bool hadInteractable = currentCollider != null || currentInteractable != null;

        currentCollider = null;
        currentInteractable = null;

        ResetInteract();

        if (hadInteractable)
        {
            OnInteractableChanged?.Invoke(null);
        }
    }

    private void StartInteraction()
    {
        OnInteractionStarted?.Invoke();
        currentInteractable.Interact();
    }

    private void ResetInteract()
    {
        interactTimer = 0.0f;
        isInteractComplete = false;
    }

    private void OnDrawGizmos()
    {
        if (camTransform == null)
        {
            return;
        }

        Gizmos.color = Color.red;

        Gizmos.DrawRay(camTransform.position, camTransform.forward * interactDistance);
    }
}