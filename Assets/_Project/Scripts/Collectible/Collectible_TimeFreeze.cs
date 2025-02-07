using UnityEngine;

public class Collectible_TimeFreeze : Collectible
{
    [Header("OBJECT ----------")]
    [Header ("References")]
    [SerializeField] private GameObject SFXPrefab_Powerup;

    [Header ("Setting")]
    [SerializeField] private float Duration = 5;
    
    public override void OnCollected()
    {
        base.OnCollected();

        GameController.Instance.FreezeTime = Duration;

        Instantiate(SFXPrefab_Powerup, transform.position, Quaternion.identity);

        base.ShowPopup("Time Freeze", transform.position);
        Destroy(gameObject);
    }
}
