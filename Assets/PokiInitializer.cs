using UnityEngine;

public class PokiInitializer : MonoBehaviour
{
    private void Start()
    {
#if !UNITY_EDITOR
        if(Application.version.Contains("poki"))
        {
            PokiUnitySDK.Instance.init();
            PokiUnitySDK.Instance.gameLoadingFinished();
        }
#endif
    }
}
