using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractable, IInteractText
{
    public abstract ItemData itemData { get; }

    public string ItemName => itemData.ItemName;

    public float InteractionDuration => 0.0f;

    public string InteractionText => "줍기 : [E]";

    // 아이템 상호작용
    public void Interact()
    {
        bool isAdded = ItemDataController.Instance.TrySendItemData(this);

        if (isAdded)
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        gameObject.SetActive(false);
    }

    public void Drop()
    { 

    }
}
