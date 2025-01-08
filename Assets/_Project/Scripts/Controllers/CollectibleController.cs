using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject Border;

    [Header("Settings")]
    [SerializeField] private Color Color;

    private void Start() => GetComponentInChildren<SpriteRenderer>().color = Color;

    private void Update() => Border.SetActive(Color == ColorController.Instance.GetColor());

    public void Collected() => Destroy(gameObject);

    public void SetColor(Color _color) => Color = _color;
    public Color GetColor() => Color;
}
