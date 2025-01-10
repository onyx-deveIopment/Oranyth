using TMPro;
using UnityEngine;

public class VersionTextController : MonoBehaviour
{
    private void Start() => GetComponent<TMP_Text>().text = "v" + Application.version;
}
