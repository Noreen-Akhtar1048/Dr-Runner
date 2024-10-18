using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public abstract class Enemy : MonoBehaviour
{

    public GameObject bulletPosition;
    public GameObject bulletPrefab;
    //public int time;
    // Start is called before the first frame update
   public virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void Shoot()
    {
          InstantiateBullet();
    }


    public GameObject projectileToIntantiate;


     void InstantiateBullet()
    {
       
        projectileToIntantiate = null;
        projectileToIntantiate= Instantiate(bulletPrefab, bulletPosition.transform);
       

    }
}
