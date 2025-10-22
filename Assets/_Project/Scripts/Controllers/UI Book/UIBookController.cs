using TMPro;
using UnityEngine;

public class UIBookController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform PagesContainer;

    [Header("Debug")]
    [SerializeField] private float CurrentPage = 0;
    [SerializeField] private float MaxPage;

    private void Start()
    {
        MaxPage = PagesContainer.childCount - 1;

        SetPageNumbers();

        ShowPage(CurrentPage);
    }

    private void SetPageNumbers() { foreach (Transform page in PagesContainer) page.Find("Page Number").gameObject.GetComponent<TMP_Text>().text = (page.GetSiblingIndex() + 1).ToString() + "/" + (MaxPage + 1).ToString(); }

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
