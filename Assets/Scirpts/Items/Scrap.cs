using UnityEngine;

public class Scrap : Item
{
    [SerializeField]
    private ScrapData scrapData;

    [SerializeField]
    private Collider scanCollider;

    private int value;
    public int Value => value;

    public override ItemData itemData => scrapData;

    private void Awake()
    {
        value = Random.Range(scrapData.MinValue, scrapData.MaxValue + 1);

        if(scanCollider == null)
        {
            scanCollider = GetComponentInChildren<Collider>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.TryGetComponent<>)
    }
}