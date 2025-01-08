using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public static PlayerController Instance;

    [Header("References")]
    [SerializeField] private SpriteRenderer SpriteRenderer;

    [Header("Settings")]
    [SerializeField] private float Acceleration = 50;
    [SerializeField] private float MaxSpeed = 10;
    [SerializeField] private float Damping = 0.9f;
    [Space]
    [SerializeField] private float OnCollectPointAmount = 1;
    [SerializeField] private float OnRemovePointAmount = 2;
    [Space]
    [SerializeField] private float OnCollectTimeAmount = 5;
    [SerializeField] private float OnRemoveTimeAmount = -10;
    [Space]
    [SerializeField] private float OnCollectShakeDuration = 0.5f;
    [SerializeField] private float OnCollectShakeFrequency = 0.1f;
    [SerializeField] private float OnCollectShakeIntensity = 0.5f;
    [Space]
    [SerializeField] private GameObject CollectRightSFXPrefab;
    [SerializeField] private GameObject CollectWrongSFXPrefab;

    [Header("Debug")]
    [SerializeField] private Vector2 Velocity;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private bool PlayerCanMove;

    private void Awake() => Instance = this;

    private void Start() => MainCamera = Camera.main;

    private void FixedUpdate()
    {
        // Movement
        if (PlayerCanMove)
        {
            Velocity += MoveInputs * Acceleration * Time.deltaTime;
            Velocity *= Damping;
            Velocity = Vector2.ClampMagnitude(Velocity, MaxSpeed);

            transform.position += (Vector3)Velocity * Time.deltaTime;
        }

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
        SpriteRenderer.color = GameController.Instance.GetColors()[GameController.Instance.GetCurrentColorIndex()];
    }

    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (!_col.gameObject.CompareTag("collectible")) return;

        CollectibleController collectibleController = _col.gameObject.GetComponent<CollectibleController>();
        collectibleController.Collected();

        CameraShake.Instance.Shake(OnCollectShakeDuration, OnCollectShakeFrequency, OnCollectShakeIntensity);

        Color color = collectibleController.GetColor();

        if (color == GameController.Instance.GetColors()[GameController.Instance.GetCurrentColorIndex()])
        {
            GameController.Instance.AddPoints(OnCollectPointAmount);
            GameController.Instance.AddTime(OnCollectTimeAmount);
            Instantiate(CollectRightSFXPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            GameController.Instance.AddPoints(OnRemovePointAmount);
            GameController.Instance.AddTime(OnRemoveTimeAmount);
            Instantiate(CollectWrongSFXPrefab, transform.position, Quaternion.identity);
        }
    }

    public void SetPlayerCanMove(bool _canMove) => PlayerCanMove = _canMove;

    public void Reset()
    {
        Velocity = Vector2.zero;
        transform.position = Vector3.zero;
    }

    #region Inputs

    [Header("Inputs")]
    [SerializeField] private Vector2 MoveInputs;

    public void OnMoveInput(InputAction.CallbackContext ctx) => MoveInputs = ctx.ReadValue<Vector2>();
    public void OnRestartInput(InputAction.CallbackContext ctx) { if (ctx.performed) GameController.Instance.RequestRestart(); }

    #endregion
}
