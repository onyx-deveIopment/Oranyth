using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [HideInInspector] public static SpawnController Instance;

    [Header("References")]
    [SerializeField] private BoxCollider2D SpawnArea;
    [SerializeField] private CircleCollider2D NoSpawnArea;
    [SerializeField] private GameObject CollectiblePrefab;

    [Header("Settings")]
    [SerializeField] private int StartCount = 20;
    [SerializeField] private float SpawnRate;

    [Header("Debug")]
    [SerializeField] private bool CanSpawn;
    [SerializeField] private float SpawnTimer;

    private Camera mainCamera;

    private void Awake() => Instance = this;

    private void Start()
    {
        SpawnArea.size = GameController.Instance.GetCameraSize();
    }

    private void Update()
    {
        if (!CanSpawn) return;

        SpawnTimer += Time.deltaTime;
        if (SpawnTimer >= SpawnRate)
        {
            SpawnTimer = 0;
            SpawnCollectible();
        }
    }

    private void SpawnBurst(int _count)
    {
        for (int i = 0; i < _count; i++)
        {
            SpawnCollectible();
        }
    }

    private void SpawnCollectible()
    {
        if (SpawnArea == null) return;

        Vector2 spawnPosition = new Vector2(
            Random.Range(SpawnArea.bounds.min.x, SpawnArea.bounds.max.x),
            Random.Range(SpawnArea.bounds.min.y, SpawnArea.bounds.max.y)
        );

        if (NoSpawnArea.OverlapPoint(spawnPosition))
        {
            SpawnCollectible();
            return;
        }

        GameObject collectible = Instantiate(CollectiblePrefab, spawnPosition, Quaternion.identity, transform);
        collectible.GetComponent<CollectibleController>().SetColor(
            GameController.Instance.GetColors()[Random.Range(0, GameController.Instance.GetColors().Length)]
        );
    }

    public void SetCanSpawn(bool _canSpawn) => CanSpawn = _canSpawn;

    public void Reset()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        SpawnBurst(StartCount);
    }
}
