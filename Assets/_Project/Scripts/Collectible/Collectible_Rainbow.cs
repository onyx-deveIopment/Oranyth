using UnityEngine;

public class Collectible_Rainbow : Collectible
{
    [Header("OBJECT ----------")]
    [Header ("References")]
    [SerializeField] private GameObject SFXPrefab_Powerup;

    [Header ("Setting")]
    [SerializeField] private float Duration = 5;
    
    public override void OnCollected()
    {
        base.OnCollected();

        GameController.Instance.RainbowTime = Duration;

        Instantiate(SFXPrefab_Powerup, transform.position, Quaternion.identity);

        base.ShowPopup("Rainbow", transform.position);
        Destroy(gameObject);
    }
}
