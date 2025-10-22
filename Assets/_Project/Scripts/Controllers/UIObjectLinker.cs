using UnityEngine;

public class UIObjectLinker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject LinkedObject;

    private void Update() { foreach(Transform _child in transform) _child.gameObject.SetActive(LinkedObject.activeInHierarchy); }
}
