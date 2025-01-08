using UnityEngine;

public class ObjectSuicide : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float LifeTime = 1;

    private void Start() => Destroy(gameObject, LifeTime);
}
