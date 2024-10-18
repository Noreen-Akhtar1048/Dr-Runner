using UnityEngine;

public class ItemCoin : MonoBehaviour, ICollectable
{
    public int amount = 1;
    public GameObject collectedFX;
    public AudioClip sound;
    bool isUsed = false;

    public void OnEnable()
    {
        PlayerIntecactions.OnCollectedEvent += PlayerIntecactions_OnCollectedEvent;
    }
    public void OnDisable()
    {
        PlayerIntecactions.OnCollectedEvent -= PlayerIntecactions_OnCollectedEvent;
    }
    private void PlayerIntecactions_OnCollectedEvent(object sender, ICollectable e)
    {
        e/*.GetComponent<ItemCoin>()*/?.OnContactPlayer();
    }

    public void OnContactPlayer()
    {
        if (isUsed)
            return;

        isUsed = true;
        GlobalValue.SavedNewCoins += amount;
        if (collectedFX)
            Instantiate(collectedFX, transform.position, Quaternion.identity);
        SoundManager.PlaySfx(sound);
        Destroy(gameObject);
    }
}