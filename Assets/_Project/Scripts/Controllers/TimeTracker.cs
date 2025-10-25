using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TimeTracker : MonoBehaviour
{
    [HideInInspector] public static TimeTracker Instance;

    [Header("Debug")]
    [SerializeField] private List<float> TimeValues = new List<float>();

    private void Awake() => Instance = this;

    private void Update()
    {
        if (GameController.Instance.IsPlaying)
        {
            TimeValues.Add(GameController.Instance.Countdown);
        }

        if (GameController.Instance.IsGameOver)
        {
            GraphController.Instance.GraphValues(TimeValues.ToArray());
            enabled = false;
        }
    }
}
