using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("COLLECTIBLE ----------")]
    [Header("Settings")]
    [SerializeField] private float DefaultColRadius = 0.75f;

    [Header("Debug")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CircleCollider2D col;

    public virtual void Start()
    {
        SetupObject();
    }

    public virtual void SetupObject()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        col = gameObject.AddComponent<CircleCollider2D>();

        rb.useAutoMass = true;
        rb.gravityScale = 0;

        col.radius = DefaultColRadius;
    }

    public virtual void OnCollected()
    {
        Debug.Log("Collectable collected!");
    }
}
