using UnityEngine;

public class RandomSeed : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [Header("Debug")]
    [SerializeField] private float Seed;

    private void Awake() => Seed = Random.Range(0, 9999);

    private void Start() => spriteRenderer.material.SetFloat("_SEED", Seed);
}
