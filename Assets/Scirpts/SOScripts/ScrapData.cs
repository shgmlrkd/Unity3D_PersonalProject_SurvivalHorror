using UnityEngine;

[CreateAssetMenu(fileName = "ScrapData", menuName = "InGameItems/ScrapData")]
public class ScrapData : ItemData
{
    [SerializeField]
    private int minValue;

    [SerializeField]
    private int maxValue;

    [SerializeField]
    private int spawnWeight;

    public int MinValue => minValue;
    public int MaxValue => maxValue;
    public int SpawnWeight => spawnWeight;
}