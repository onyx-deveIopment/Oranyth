using TMPro;
using UnityEngine;

public class Collectible_MultiPoint : Collectible
{
    [Header("OBJECT ----------")]
    [Header ("References")]
    [SerializeField] private GameObject SFXPrefab_Collect;
    [SerializeField] private TMP_Text CountText;

    [Header("Setting")]
    [SerializeField] private float Timing = 0.5f;
    [SerializeField] private int CollectAmount = 5;

    [Header("Debug")]
    [SerializeField] private bool Collected;
    [SerializeField] private float Timer;

    public void Update()
    {
        CountText.text = "+" + CollectAmount.ToString();

        if (!Collected) return;
        Timer -= Time.deltaTime;
        if (Timer > 0) return;

        Timer = Timing;

        base.ShowPopup("+1", transform.position);
        Instantiate(SFXPrefab_Collect, transform.position, Quaternion.identity);
        CameraEffect_Shake.Instance.Shake(base.shakeSettings);
        GameController.Instance.AddTime(1);

        CollectAmount--;
        if (CollectAmount > 0) return;
        Destroy(gameObject);
    }

    public override void OnCollected()
    {
        if(Collected) return;

        base.OnCollected();

        Collected = true;
    }
}
