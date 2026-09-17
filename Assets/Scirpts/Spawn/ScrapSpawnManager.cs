using UnityEngine;

public class ScrapSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] scrapPrefabs;

    [SerializeField]
    private int spawnCount = 10;

    [SerializeField]
    private float maxDistance = 5.0f;

    [SerializeField]
    private LayerMask floorLayer;

    private ScrapSpawnArea[] spawnAreas;

    private GameObject scrapRoot;

    private void Awake()
    {
        spawnAreas = GetComponentsInChildren<ScrapSpawnArea>();

        scrapRoot = new GameObject("ScrapRoot");
        scrapRoot.transform.SetParent(transform);
    }

    private void Start()
    {
        SpawnScraps();
    }

    private void SpawnScraps()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnScrap();
        }
    }

    private void SpawnScrap()
    {
        ScrapSpawnArea area = spawnAreas[Random.Range(0, spawnAreas.Length)];

        Vector3 rayStartPosition = area.GetRandomPosition();

        if (Physics.Raycast(rayStartPosition, Vector3.down, out RaycastHit hit, maxDistance, floorLayer))
        {
            GameObject scrap = scrapPrefabs[Random.Range(0, scrapPrefabs.Length)];

            Instantiate(scrap, hit.point, scrap.transform.rotation, scrapRoot.transform);
        }
    }
}
