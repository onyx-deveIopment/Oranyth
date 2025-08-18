#if UNITY_EDITOR
using UnityEngine;

[CreateAssetMenu(fileName = "VersionSettings", menuName = "ONYX/Version Settings")]
public class VersionSettings : ScriptableObject
{
    [Header("Version Settings")]
    [SerializeField] public string buildNumber = "1.0.0";
    [SerializeField] public Distribution distribution = Distribution.None;

    public enum Distribution { None, AndrewArcade, Poki }
}
#endif