using UnityEngine;

public class ItemDataController : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    public static ItemDataController Instance { get; private set; }

    [SerializeField]
    private PlayerInventory playerInventory;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if(playerInventory == null)
        {
            playerInventory = GameObject.FindGameObjectWithTag(PLAYER_TAG).GetComponent<PlayerInventory>();
        }
    }

    public bool TrySendItemData(Item item)
    {
        return playerInventory.TryAddItem(item.itemData);
    }
}