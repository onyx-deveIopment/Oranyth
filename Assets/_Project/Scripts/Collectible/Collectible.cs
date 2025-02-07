using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("COLLECTIBLE ----------")]
    [Header("Settings")]
    [SerializeField] private float DefaultColRadius = 0.75f;
    [SerializeField] private GameObject PopupPrefab;


    [SerializeField]
    private CameraEffect_Shake_Settings shakeSettings = new CameraEffect_Shake_Settings()
    {
        Duration = 0.2f,
        Frequency = 0.05f,
        Intensity = 0.25f
    };
    
    [Header("Debug")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CircleCollider2D col;


    public virtual void Start()
    {
        SetupObject();
    }

    public virtual void SetupObject()
    {
        gameObject.tag = "collectible";

        rb = gameObject.AddComponent<Rigidbody2D>();
        col = gameObject.AddComponent<CircleCollider2D>();

        rb.useAutoMass = true;
        rb.gravityScale = 0;

        col.radius = DefaultColRadius;
    }

    public virtual void OnCollected()
    {
        Debug.Log("Collectable collected!");
        CameraEffect_Shake.Instance.Shake(shakeSettings);
    }

    public void ShowPopup(string _text, Vector3 _position)
    {
        GameObject popup = Instantiate(PopupPrefab, transform.position, Quaternion.identity);

        PopupController popupController = popup.GetComponent<PopupController>();
        popupController.SetMessage(_text);
        popupController.GoToObject(_position);
    }
}