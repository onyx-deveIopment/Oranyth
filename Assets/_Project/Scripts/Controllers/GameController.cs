using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector] public static GameController Instance;

    [Header("References")]
    [SerializeField] private TMP_Text[] PointsText;
    [SerializeField] private TMP_Text CountdownText;
    [SerializeField] private GameObject TitleScreen;

    [Header("Settings")]
    [SerializeField] private Color[] Colors;
    [SerializeField] private float ColorChangeRate;
    [SerializeField] private float CountdownStart = 30;
    [Space]
    [SerializeField] private float GameOverShakeDuration = 1;
    [SerializeField] private float GameOverShakeFrequency = 0.1f;
    [SerializeField] private float GameOverShakeIntensity = 1;
    [Space]
    [SerializeField] private GameObject GameOverSFXPrefab;
    [SerializeField] private Animator EndScreenAnimator;

    [Header("Debug")]
    [SerializeField] private int CurrentColorIndex;
    [SerializeField] private float ColorChangeTimer;
    [SerializeField] private float Points;
    [SerializeField] private float Countdown;
    [SerializeField] private GameState State = GameState.Title;

    private enum GameState
    {
        Title,
        Playing,
        GameOver
    }

    private void Awake() => Instance = this;

    private void Start()
    {
        TitleScreen.SetActive(true);
    }

    private void Update()
    {
        if (State == GameState.Playing)
        {
            // Color
            ColorChangeTimer += Time.deltaTime;
            if (ColorChangeTimer >= ColorChangeRate)
            {
                ColorChangeTimer = 0;
                CurrentColorIndex = Random.Range(0, Colors.Length);
            }

            // Time
            Countdown -= Time.deltaTime;
            if (Countdown <= 0) GameOver();

            // Points
            if (Points < 0) GameOver();
        }

        // UI
        foreach (TMP_Text text in PointsText) text.text = Points.ToString();
        CountdownText.text = (Mathf.Round(Countdown * 100) / 100).ToString("F2");
    }

    private void StartGame()
    {
        State = GameState.Playing;

        PlayerController.Instance.Reset();
        SpawnController.Instance.Reset();

        AddTime(CountdownStart);

        TitleScreen.SetActive(false);

        PlayerController.Instance.SetPlayerCanMove(true);
        SpawnController.Instance.SetCanSpawn(true);
    }

    private void GameOver()
    {
        State = GameState.GameOver;

        Instantiate(GameOverSFXPrefab);
        EndScreenAnimator.SetBool("show", true);
        CameraShake.Instance.Shake(GameOverShakeDuration, GameOverShakeFrequency, GameOverShakeIntensity);

        Countdown = 0;
        if (Points < 0) Points = 0;

        PlayerController.Instance.SetPlayerCanMove(false);
        SpawnController.Instance.SetCanSpawn(false);
    }

    public void RequestRestart()
    {
        if (State == GameState.Title)
        {
            StartGame();
            return;
        }


        if (State == GameState.Playing) return;

        EndScreenAnimator.SetBool("show", false);
        Points = 0;
        Countdown = CountdownStart;
        StartGame();
    }

    public void AddPoints(float _points) => Points += _points;

    public Color[] GetColors() => Colors;
    public int GetCurrentColorIndex() => CurrentColorIndex;

    public void AddTime(float _time) => Countdown += _time;

    public Vector2 GetCameraSize()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            Vector3 screenBottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
            Vector3 screenTopRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            Vector2 areaSize = new Vector2(
                screenTopRight.x - screenBottomLeft.x,
                screenTopRight.y - screenBottomLeft.y
            );

            return areaSize;
        }

        return Vector2.zero;
    }
}
