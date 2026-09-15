using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "InGameItems/ItemData")]
public abstract class ItemData : ScriptableObject
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private float weight;

    [SerializeField]
    private bool isTwoHand;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private AudioClip pickupSound;

    [SerializeField]
    private AudioClip dropSound;

    public string ItemName => itemName;
    public float Weight => weight;
    public bool IsTwoHand => isTwoHand;
    public GameObject Prefab => prefab;

    public AudioClip PickupSound => pickupSound;
    public AudioClip DropSound => dropSound;
}