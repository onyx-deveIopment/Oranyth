using UnityEngine;

public class Collectible_Dot : Collectible
{
    [Header("OBJECT ----------")]
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject Border;
    [Space]
    [SerializeField] private GameObject PopupPrefab;
    [Space]
    [SerializeField] private GameObject SFXPrefab_Correct;
    [SerializeField] private GameObject SFXPrefab_Wrong;

    [Header("Settings")]
    [SerializeField] private float CorrectTimeAmount = 1;
    [SerializeField] private float RemoveTimeAmount = -5;

    [Header("Debug")]
    [SerializeField] private Color Color;

    public override void SetupObject()
    {
        base.SetupObject();

        Color = ColorController.Instance.GetRandomColor();

        spriteRenderer.color = Color;
    }

    private void Update()
    {
        Border.SetActive(Color == ColorController.Instance.GetColor());
    }

    public override void OnCollected()
    {
        base.OnCollected();

        if (Color == ColorController.Instance.GetColor()) OnCorrect(); else OnWrong();

        Destroy(gameObject);
    }

    private void OnCorrect()
    {
        GameController.Instance.AddTime(CorrectTimeAmount);
        ShowPopup("+" + CorrectTimeAmount.ToString());

        GameController.Instance.Collect(true);

        Instantiate(SFXPrefab_Correct, transform.position, Quaternion.identity);
    }

    private void OnWrong()
    {
        GameController.Instance.AddTime(RemoveTimeAmount);
        ShowPopup(RemoveTimeAmount.ToString());
        
        GameController.Instance.Collect(false);

        Instantiate(SFXPrefab_Wrong, transform.position, Quaternion.identity);
    }

    private void ShowPopup(string _text)
    {
        GameObject popup = Instantiate(PopupPrefab, transform.position, Quaternion.identity);

        PopupController popupController = popup.GetComponent<PopupController>();
        popupController.SetMessage(_text);
        popupController.GoToObject(transform.position);
    }
}