using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private ItemData[] items = new ItemData[4];

    private void OnEnable()
    {
        ItemDataController.Instance.OnItemSend += AddItem;
    }

    private void OnDisable()
    {
        ItemDataController.Instance.OnItemSend -= AddItem;
    }

    private void AddItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                continue;
            }

            items[i] = item.itemData;

            item.PickUp();

            Debug.Log($"Slot {i} : {item.itemData.ItemName}");
            break;
        }
    }
}
