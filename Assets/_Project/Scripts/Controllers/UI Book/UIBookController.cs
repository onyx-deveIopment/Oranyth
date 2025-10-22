using UnityEngine;

public class UIBookController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform PagesContainer;

    [Header("Debug")]
    [SerializeField] private float CurrentPage;
    [SerializeField] private float MaxPage;

    private void Start() => MaxPage = PagesContainer.childCount - 1;

    private void ShowPage(float _PageNumber)
    {
        foreach (Transform page in PagesContainer) page.gameObject.SetActive(false);
        PagesContainer.GetChild((int)_PageNumber).gameObject.SetActive(true);
    }

    public void OnNextPage_ButtonPressed()
    {
        CurrentPage = Mathf.Min(CurrentPage + 1, MaxPage);
        ShowPage(CurrentPage);
    }
    
    public void OnPreviousPage_ButtonPressed()
    {
        CurrentPage = Mathf.Max(CurrentPage - 1, 0);
        ShowPage(CurrentPage);
    }
}
