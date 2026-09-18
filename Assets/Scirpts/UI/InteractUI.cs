using TMPro;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField]
    private GameObject interactImage;

    [SerializeField]
    private TextMeshProUGUI interactText;

    [SerializeField]
    private PlayerInteract playerInteract;

    private IInteractText currentText;
    private IInteractState currentState;

    private void Awake()
    {
        Hide();
    }

    private void OnEnable()
    {
        if (playerInteract != null)
        {
            playerInteract.OnInteractableChanged += SetInteractable;
            playerInteract.OnInteractionStarted += Hide;
        }
    }

    private void OnDisable()
    {
        if (playerInteract != null)
        {
            playerInteract.OnInteractableChanged -= SetInteractable;
            playerInteract.OnInteractionStarted -= Hide;
        }

        UnsubscribeCurrentState();
    }

    private void SetInteractable(IInteractable interactable)
    {
        UnsubscribeCurrentState();

        if (interactable == null)
        {
            Hide();
            return;
        }

        currentText = interactable as IInteractText;
        currentState = interactable as IInteractState;

        if (currentText == null)
        {
            Hide();
            return;
        }

        if (currentState != null)
        {
            currentState.OnChanged += Refresh;
        }

        Refresh();
    }

    private void Refresh()
    {
        if (currentText == null)
        {
            Hide();
            return;
        }

        interactImage.SetActive(true);
        interactText.gameObject.SetActive(true);
        interactText.text = currentText.InteractionText;
    }

    private void UnsubscribeCurrentState()
    {
        if (currentState == null)
        {
            return;
        }

        currentState.OnChanged -= Refresh;
        currentState = null;
        currentText = null;
    }

    private void Hide()
    {
        interactImage.SetActive(false);
        interactText.gameObject.SetActive(false);
    }
}