#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VersionSettings))]
public class VersionSettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        VersionSettings settings = (VersionSettings)target;

        if (GUILayout.Button("Apply to Project Settings"))
        {
            ApplyBuildSettings(settings);
        }
    }

    private void ApplyBuildSettings(VersionSettings settings)
    {
        string version = settings.buildNumber +
                        (settings.distribution == VersionSettings.Distribution.AndrewArcade ? "-andrewarcade" :
                        (settings.distribution == VersionSettings.Distribution.Poki         ? "-poki" : ""));

        PlayerSettings.bundleVersion = version;
        
        Debug.Log($"[BuildSettings] Build version set to: \"{version}\".");
    }
}
#endif
