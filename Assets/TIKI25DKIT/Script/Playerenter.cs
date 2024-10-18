using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerenter : MonoBehaviour
{
    public Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("player Enter");
            anim.SetBool("Attack",true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("player Exit");
            anim.SetBool("Attack",false);
        }
    }
}
