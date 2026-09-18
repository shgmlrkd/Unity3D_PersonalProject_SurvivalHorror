using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable, IInteractText, IInteractState
{
    [SerializeField]
    private float rotateDuration = 0.2f;

    private bool isOpen;

    private Quaternion closeRotation;
    private Quaternion openRotation;

    private Coroutine doorRotateCoroutine;

    public float InteractionDuration => 0.5f;

    public string InteractionText => isOpen ? "문 닫기 : [E]" : "문 열기 : [E]";

    public event Action OnChanged;

    private void Awake()
    {
        closeRotation = transform.localRotation;

        openRotation = closeRotation * Quaternion.Euler(0.0f, 90.0f, 0.0f);
    }

    public void Interact()
    {
        if (doorRotateCoroutine != null)
        {
            return;
        }

        doorRotateCoroutine = StartCoroutine(DoorRotateCo());
    }

    private IEnumerator DoorRotateCo()
    {
        Quaternion startRotation = transform.localRotation;

        Quaternion targetRotation = isOpen ? closeRotation : openRotation;

        float timer = 0.0f;

        while (timer < rotateDuration)
        {
            timer += Time.deltaTime;

            float ratio = timer / rotateDuration;

            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, ratio);

            yield return null;
        }

        transform.localRotation = targetRotation;

        isOpen = !isOpen;

        doorRotateCoroutine = null;

        // 문 상태가 바뀌었음을 UI에 알림
        OnChanged?.Invoke();
    }
}