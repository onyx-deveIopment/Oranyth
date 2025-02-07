using UnityEngine;

public class Collectible_TimeFreeze : Collectible
{
    [Header("OBJECT ----------")]
    [Header ("Setting")]
    [SerializeField] private float duration = 5;
    
    public override void OnCollected()
    {
        base.OnCollected();

        GameController.Instance.FreezeTime = duration;

        base.ShowPopup("Time Freeze", transform.position);
        Destroy(gameObject);
    }
}
