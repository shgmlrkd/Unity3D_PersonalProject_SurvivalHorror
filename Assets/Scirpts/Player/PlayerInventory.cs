using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private ItemData[] items = new ItemData[4];

    public bool TryAddItem(ItemData itemData)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                continue;
            }

            items[i] = itemData;

            Debug.Log($"Slot {i} : {itemData.ItemName}");
            return true;
        }

        Debug.Log("인벤토리가 가득 찼습니다.");
        return false;
    }
}