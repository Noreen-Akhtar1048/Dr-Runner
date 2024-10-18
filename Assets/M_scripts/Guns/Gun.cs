using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Purchasing;

public abstract class Gun : MonoBehaviour
{
    public GunSO GunSO;
    public float FireRate;
    public float damage;
    public float magzineCapacity;
    public int gunid;
   // public int price;
    public GameObject bulletPrefab;
    public ParticleSystem MuzzleVfx;
    public GameObject bulletPosiion;
    public float currentCapacity;
    public static Gun instance;

    private void Awake()
    {
        instance = this;
      SetGunProperties();
    }
    // Start is called before the first frame update
    public  virtual  void Start()
    {
        MenuManager.Instance.BulletText.text = magzineCapacity.ToString();

        SetGunProperties();
        MuzzleVfx.Stop();
    }
    private void OnEnable()
    {
       
        magzineCapacity = GunSO.magzineCapacity;
    }
    public void ResetMagzine()
    {
        magzineCapacity = GunSO.magzineCapacity;
        UpdateMagzineText(magzineCapacity);
    }
    // Update is called once per frame
    void Update()
    {


    }
    void SetGunProperties()
    {
        magzineCapacity = GunSO.magzineCapacity;
        // print("i am called");
        currentCapacity = GunSO.magzineCapacity;
        FireRate = GunSO.FireRate;
        damage=GunSO.damage;
        gunid = GunSO.GunId;
        //price= GunSO.price;
       // MuzzleVfx = GunSO.MuzzleVfx;
       
         
    }
    public GameObject projectileToIntantiate;

    public virtual void Shoot()
    {
       
        if (magzineCapacity >  0)
        {

            InstantiateBullet();  
            magzineCapacity -= 1;
                MuzzleVfx.Play();
            print("INSTANTIATED");
           // MenuManager.Instance.BulletText.text = magzineCapacity.ToString();
            PlayerController.instance.anim.SetTrigger("Fire");
            UpdateMagzineText(magzineCapacity);
        }
       

        Debug.Log("fire");
        
    }
    async void InstantiateBullet()
    {
        await Task.Delay(80);
        projectileToIntantiate = null;
        projectileToIntantiate = Instantiate(bulletPrefab, bulletPosiion.transform);

    }
    public virtual void reaload()
    {
        magzineCapacity = GunSO.magzineCapacity;
    }

    void UpdateMagzineText(float value)
    {
       MenuManager.Instance.BulletText.text = value.ToString();
    }



}
