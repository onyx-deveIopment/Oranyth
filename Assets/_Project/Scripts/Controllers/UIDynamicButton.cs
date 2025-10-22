using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDynamicButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button TopLeftButton;
    [SerializeField] private Button TopRightButton;

    [Header("Debug")]
    [SerializeField] private Button ThisButton;
    [SerializeField] private Button PreviousButton;

    private void Start()
    {
        ThisButton = GetComponent<Button>();
        PreviousButton = (Button)ThisButton.navigation.selectOnUp;
    }

    private void Update()
    {
        var nav = ThisButton.navigation;
        nav.selectOnUp = PreviousButton;
        ThisButton.navigation = nav;
    }

    public void SetPreviousButton(Button _PreviousButton) => PreviousButton = _PreviousButton;
}
