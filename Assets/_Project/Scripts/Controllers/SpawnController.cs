using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [HideInInspector] public static SpawnController Instance;

    [Header("References")]
    [SerializeField] private BoxCollider2D SpawnArea;
    [SerializeField] private CircleCollider2D NoSpawnArea;
    [SerializeField] private List<SpawnableObject> spawnableObjects;

    [Header("Settings")]
    [SerializeField] private int StartCount = 20;
    [SerializeField] private float SpawnRate;

    [Header("Debug")]
    [SerializeField] private float SpawnTimer;
    [SerializeField] private bool SpawnerEnabled = true;

    private Camera mainCamera;

    [System.Serializable]
    public class SpawnableObject
    {
        public int chanceOfSpawn = 100;
        public GameObject prefab;
    }

    private void Awake() => Instance = this;

    private void Start()
    {
        SpawnArea.size = GetCameraSize();
        SpawnBurst(StartCount);
    }

    private void Update()
    {
        if (!SpawnerEnabled) return;

        SpawnTimer += Time.deltaTime;
        if (SpawnTimer >= SpawnRate)
        {
            SpawnTimer = 0;
            SpawnCollectibles();
        }
    }

    private void SpawnBurst(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnCollectibles();
        }
    }

    private void SpawnCollectibles()
    {
        if (SpawnArea == null || spawnableObjects == null || spawnableObjects.Count == 0) return;

        foreach (var spawnable in spawnableObjects)
        {
            if (Random.Range(0, 100) < spawnable.chanceOfSpawn)
            {
                Vector2 spawnPosition = GetValidSpawnPosition();
                if (spawnPosition != Vector2.zero)
                {
                    Instantiate(spawnable.prefab, spawnPosition, Quaternion.identity, transform);
                }
            }
        }
    }

    private Vector2 GetValidSpawnPosition()
    {
        for (int attempt = 0; attempt < 10; attempt++)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(SpawnArea.bounds.min.x, SpawnArea.bounds.max.x),
                Random.Range(SpawnArea.bounds.min.y, SpawnArea.bounds.max.y)
            );

            if (!NoSpawnArea.OverlapPoint(spawnPosition))
            {
                return spawnPosition;
            }
        }
        return Vector2.zero;
    }

    public void DisableSpawner() => SpawnerEnabled = false;

    public void Reset()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        SpawnBurst(StartCount);
    }

    public Vector2 GetCameraSize()
    {
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Vector3 screenBottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
            Vector3 screenTopRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            return new Vector2(
                screenTopRight.x - screenBottomLeft.x,
                screenTopRight.y - screenBottomLeft.y
            );
        }
        return Vector2.zero;
    }
}
