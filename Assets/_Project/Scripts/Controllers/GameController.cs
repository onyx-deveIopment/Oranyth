using System.Drawing;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector] public static GameController Instance;

    [Header("References")]
    [SerializeField] private TMP_Text[] PointsText;
    [SerializeField] private TMP_Text CountdownText;

    [Header("Settings")]
    [SerializeField] private int StartTime = 60;

    [Header("Debug")]
    [SerializeField] private int Points = 0;
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
    }

    private void Update()
    {
        if (State != GameState.Playing) return;

        Countdown -= Time.deltaTime;

        if (Countdown <= 0) GameOver();
        if (Points < 0) GameOver();

        UpdateUI();
    }

    private void GameOver()
    {
        Debug.Log("Game Over");

        State = GameState.GameOver;

        PlayerController.Instance.DisablePlayer();
        SpawnController.Instance.DisableSpawner();

        Countdown = 0;
        if (Points < 0) Points = 0;

        UpdateUI();

        UIPanelController.Instance.ShowPanel(1);

        Cursor.visible = true;
    }

    private void UpdateUI(){
        foreach (TMP_Text text in PointsText) text.text = Points.ToString();

        CountdownText.text = (Mathf.Round(Countdown * 100) / 100).ToString("F2");
    }

    public void AddPoints(int _amount) => Points += _amount;
    public void AddTime(float _amount) => Countdown += _amount;
}
