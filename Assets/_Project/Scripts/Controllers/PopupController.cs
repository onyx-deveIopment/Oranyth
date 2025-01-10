using TMPro;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text PopupText;

    [Header("Debug")]
    [SerializeField] private string PopupMessage;

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        transform.parent = GameObject.FindGameObjectWithTag("popup_container").transform;

        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void Update() => PopupText.text = PopupMessage;

    public void SetMessage(string _text) => PopupMessage = _text;

    public void GoToObject(Vector3 _position)
    {
        if (canvas == null) return;

        // Convert world position to screen position
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(_position);
        screenPosition.z = 0;

        // Convert screen position to UI position
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 uiPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPosition,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
            out uiPosition
        );

        // Set the position of this UI object
        rectTransform.anchoredPosition = uiPosition;
    }
}
