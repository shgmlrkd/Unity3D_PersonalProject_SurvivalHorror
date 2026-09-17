using UnityEngine;

public class ScrapSpawnArea : MonoBehaviour
{
    private BoxCollider spawnArea;

    private void Awake()
    {
        spawnArea = GetComponent<BoxCollider>();
    }

    public Vector3 GetRandomPosition()
    {
        Bounds bounds = spawnArea.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, bounds.max.y, randomZ);
    }
}
