using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public static PlayerController Instance;

    [Header("References")]
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private GameObject PopupPrefab;

    [Header("Settings")]
    [SerializeField] private float Acceleration = 50;
    [SerializeField] private float MaxSpeed = 10;
    [SerializeField] private float Damping = 0.9f;

    [Header("Debug")]
    [SerializeField] private Vector2 Velocity;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private bool PlayerEnabled = true;

    private void Awake() => Instance = this;

    private void Start()
    {
        MainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (!PlayerEnabled) return;

        // Movement
        Velocity += MoveInputs * Acceleration * Time.deltaTime;
        Velocity *= Damping;
        Velocity = Vector2.ClampMagnitude(Velocity, MaxSpeed);

        transform.position += (Vector3)Velocity * Time.deltaTime;

        // Clamp the player's position within camera bounds
        Vector3 clampedPosition = transform.position;
        Vector3 screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float spriteWidth = SpriteRenderer.bounds.extents.x;
        float spriteHeight = SpriteRenderer.bounds.extents.y;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x + spriteWidth, screenBounds.x - spriteWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y + spriteHeight, screenBounds.y - spriteHeight);

        transform.position = clampedPosition;
    }

    private void Update()
    {
        // Graphics
        SpriteRenderer.color = ColorController.Instance.GetColor();
    }

    private void OnTriggerEnter2D(Collider2D _col) { if (_col.gameObject.CompareTag("collectible")) _col.gameObject.GetComponent<Collectible>().OnCollected(); }

    public void DisablePlayer() => PlayerEnabled = false;

    #region Inputs

    [Header("Inputs")]
    [SerializeField] private Vector2 MoveInputs;

    public void OnMoveInput(InputAction.CallbackContext ctx) => MoveInputs = ctx.ReadValue<Vector2>();

    #endregion
}
