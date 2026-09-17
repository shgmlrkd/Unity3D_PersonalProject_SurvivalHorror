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

    private void Awake()
    {
        camTransform = Camera.main.transform;
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
            // 계속 같은 Collider를 보고 있다면 다시 찾지 않음
            if (currentCollider == hit.collider)
            {
                return;
            }

            currentCollider = hit.collider;
            currentInteractable = hit.collider.GetComponentInParent<IInteractable>();

            ResetInteract();

            return;
        }

        if (currentCollider != null)
        {
            currentCollider = null;
            currentInteractable = null;

            ResetInteract();
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
                currentInteractable.Interact();
                print("상호작용");
            }

            return;
        }

        // 문 열기 상호작용
        if (Keyboard.current.eKey.isPressed)
        {
            interactTimer += Time.deltaTime;

            if (!isInteractComplete && interactTimer >= currentInteractable.InteractionDuration)
            {
                currentInteractable.Interact();

                isInteractComplete = true;
            }
        }

        if (Keyboard.current.eKey.wasReleasedThisFrame)
        {
            ResetInteract();
        }
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
