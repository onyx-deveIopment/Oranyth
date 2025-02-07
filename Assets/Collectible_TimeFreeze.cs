using UnityEngine;

public class Collectible_TimeFreeze : Collectible
{
    [Header("OBJECT ----------")]
    [Header ("Setting")]
    [SerializeField] private float duration = 5;
    
    [Header ("Debug")]
    [SerializeField] private bool collected = false;

    private void Update()
    {
        if(!collected) return;

        duration -= Time.deltaTime;

        GameController.Instance.freezeTime = true;

        if(duration <= 0)
        {
            GameController.Instance.freezeTime = false;
            Destroy(gameObject);
        }
    }

    public override void OnCollected()
    {
        base.OnCollected();

        collected = true;

        Debug.Log("Yes.");
    }
}
