using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [HideInInspector] public static GameController Instance;

    [Header("References")]
    [SerializeField] private TMP_Text TotalTimeText;
    [SerializeField] private TMP_Text CorrectCountText;
    [SerializeField] private TMP_Text WrongCountText;
    [SerializeField] private TMP_Text CountdownText;
    [SerializeField] private GameObject GameOverSFX;
    [SerializeField] private Slider ColorSlider;
    [SerializeField] private Image ColorNobImage;

    [Header("Settings")]
    [SerializeField] private int StartTime = 60;

    [Header("Debug")]
    [SerializeField] private float TotalTime = 0;
    [SerializeField] private int CorrectCount = 0;
    [SerializeField] private int WrongCount = 0;
    [SerializeField] public float FreezeTime = 0;
    [SerializeField] public float RainbowTime = 0;

    [SerializeField] private float Countdown;

    [SerializeField] private GameState State = GameState.Playing;

    private enum GameState
    {
        Playing,
        GameOver
    }

    private void Awake() => Instance = this;

    private void Start()
    {
        Countdown = StartTime;

        Cursor.visible = false;

#if !UNITY_EDITOR
        if(Application.version.Contains("poki"))
            PokiUnitySDK.Instance.gameplayStart();
#endif
    }

    private void Update()
    {
        if (State != GameState.Playing) return;

        TotalTime += Time.deltaTime;

        RainbowTime -= Time.deltaTime;

        FreezeTime -= Time.deltaTime;
        if(FreezeTime <= 0) Countdown -= Time.deltaTime;

        if (Countdown <= 0) GameOver();

        UpdateUI();
    }

    private void GameOver()
    {
        State = GameState.GameOver;

        PlayerController.Instance.DisablePlayer();
        SpawnController.Instance.DisableSpawner();

        Countdown = 0;

#if UNITY_EDITOR
        GameOver_End();
#else
        if(Application.version.Contains("poki"))
        {
            PokiUnitySDK.Instance.gameplayStop();
            PokiUnitySDK.Instance.commercialBreakCallBack = GameOver_End;
            PokiUnitySDK.Instance.commercialBreak();
        }else{
            GameOver_End();
        }
#endif
    }

    public void GameOver_End()
    {
        Debug.Log("Game Over");
        UpdateUI();

        Instantiate(GameOverSFX);

        UIPanelController.Instance.ShowPanel(1);

        Cursor.visible = true;
    }

    private void UpdateUI()
    {
        TotalTimeText.text = (Mathf.Round(TotalTime * 100) / 100).ToString("F2");

        CorrectCountText.text = CorrectCount.ToString();
        WrongCountText.text = WrongCount.ToString();

        ColorSlider.value = ColorController.Instance.GetTimeRatio();
        ColorNobImage.color = ColorController.Instance.GetNextColor();

        CountdownText.text = (Mathf.Round(Countdown * 100) / 100).ToString("F2");
    }

    public void Collect(bool _correct) { if (_correct) CorrectCount++; else WrongCount++; }
    public void AddTime(float _amount) => Countdown += _amount;
}
