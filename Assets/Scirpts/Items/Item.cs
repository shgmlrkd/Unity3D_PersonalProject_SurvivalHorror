using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractable
{
    public abstract ItemData itemData { get; }

    public string ItemName => itemData.ItemName;

    public void Interact()
    {
        // 상호작용
    }
}
