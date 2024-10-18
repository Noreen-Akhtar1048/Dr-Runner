using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class Life : MonoBehaviour,IDamageAble
{
    public TextMesh Lifetxt;
    public GameObject life;
    public float health;
    public static event EventHandler<GameObject> OndiedEvent;
    public static event EventHandler<GameObject> OnDamageEvent;
    
    public void Damage(float damage)
    {
        health -= damage;
        OnDamageEvent?.Invoke(this, this.gameObject);
        Debug.Log("damage has been done" + gameObject);
        if(health <= 0)
        {
            OndiedEvent?.Invoke(this, this.gameObject);
            life.SetActive(false);
            //getcomponent and call kill
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health>0)
        {
            Lifetxt.text = health.ToString();
            Lifetxt.transform.rotation = Quaternion.identity;
           
        }
        
    }
}
