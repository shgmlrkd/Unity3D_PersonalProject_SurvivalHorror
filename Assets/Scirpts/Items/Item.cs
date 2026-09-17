using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractable
{
    public abstract ItemData itemData { get; }

    public string ItemName => itemData.ItemName;

    public float InteractionDuration => 0.0f;

    // 아이템 상호작용
    public void Interact()
    {
        ItemDataController.Instance.SendItemData(this);
    }

    public void PickUp()
    {
        gameObject.SetActive(false);
    }

    public void Drop()
    { 

    }
}
