using UnityEngine;

public class AndrewArcadeInitializer : MonoBehaviour
{
    private void Start()
    {
#if !UNITY_EDITOR
        if(Application.version.Contains("andrewarcade"))
            Cursor.visible = false;
#endif
    }
}
