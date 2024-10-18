using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFuel : TriggerEvent
{
    //public int amount = 1;
    public GameObject collectedFX;
    public AudioClip sound;
    bool isUsed = false;

    public override void OnContactPlayer()
    {
        
        
        print("Tasks: " + MenuManager.tasksDone);

        if (isUsed)
            return;

        isUsed = true;
        //GlobalValue.Bullets += amount;
        if (collectedFX)
            Instantiate(collectedFX, transform.position, Quaternion.identity);
        SoundManager.PlaySfx(sound);
        MenuManager.tasksDone++;
        
        Destroy(gameObject);
    }
}
