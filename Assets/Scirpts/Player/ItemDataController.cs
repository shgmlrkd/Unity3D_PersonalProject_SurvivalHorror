using System;
using UnityEngine;

public class ItemDataController : MonoBehaviour
{
    public static ItemDataController Instance { get; private set; }

    public event Action<Item> OnItemSend;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SendItemData(Item item)
    {
        OnItemSend?.Invoke(item);
    }
}