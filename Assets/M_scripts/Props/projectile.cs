using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;


public abstract class projectile : MonoBehaviour
{
    public bool isEnemyProjectile;
    public enum BulletType
    {
        Akm,Vector,Shotgun,Rocket, Enemy1, Enemy2, Enemy3, Enemy4, Enemy5, Enemy6, Enemy7, Enemy8, Enemy9

    }
    public BulletType type;

    public ProjectileScriptableSO[] ProjectileScriptableSO;

    [SerializeField] GameObject ProjectileVisual;
   public float damage;
   public float projectileSpeed;
    [SerializeField] GameObject ProjectileVfx;
    [SerializeField] Vector3 direction;
    
    public static Action onbombExplode;

    public virtual void Start()
    {

        Populate();
    }
    
    async void Populate()
    {
      await System.Threading.Tasks.Task.Delay(100);
    
            //    type = (BulletType)WeaponSelector.currentWeapon;
            switch (type)

            {
                case BulletType.Akm:

                    ProjectileVisual = Instantiate(ProjectileScriptableSO[0].ProjectileVisual);
                    ProjectileVfx = ProjectileScriptableSO[0].ProjectileVfx;
                    ProjectileVisual.transform.position = transform.position;
                    ProjectileVisual.transform.SetParent(transform);
                    damage = ProjectileScriptableSO[0].damage;
                    ProjectileVisual.transform.localEulerAngles = ProjectileScriptableSO[0].VisualRotation;
                    projectileSpeed = ProjectileScriptableSO[0].projectileSpeed;
                    
                    break;
                case BulletType.Vector:

                    ProjectileVisual = Instantiate(ProjectileScriptableSO[1].ProjectileVisual);
                    ProjectileVfx = ProjectileScriptableSO[1].ProjectileVfx;
                    ProjectileVisual.transform.position = transform.position;
                    ProjectileVisual.transform.SetParent(transform);
                    damage = ProjectileScriptableSO[1].damage;
                    projectileSpeed = ProjectileScriptableSO[1].projectileSpeed;
                  
                    break;
                case BulletType.Shotgun:

                    ProjectileVisual = Instantiate(ProjectileScriptableSO[2].ProjectileVisual);
                    ProjectileVfx = ProjectileScriptableSO[2].ProjectileVfx;
                    ProjectileVisual.transform.position = transform.position;
                    ProjectileVisual.transform.SetParent(transform);
                    damage = ProjectileScriptableSO[2].damage;
                    projectileSpeed = ProjectileScriptableSO[2].projectileSpeed;
              
                    break;
                case BulletType.Rocket:
                    ProjectileVisual = Instantiate(ProjectileScriptableSO[3].ProjectileVisual);
                    ProjectileVfx = ProjectileScriptableSO[3].ProjectileVfx;
                    ProjectileVisual.transform.position = transform.position;
                    ProjectileVisual.transform.SetParent(transform);
                    damage = ProjectileScriptableSO[3].damage;
                    projectileSpeed = ProjectileScriptableSO[3].projectileSpeed;
                
                    break;
                case BulletType.Enemy1:
                    ProjectileVisual = Instantiate(ProjectileScriptableSO[4].ProjectileVisual);
                    ProjectileVisual.transform.position = transform.position;
                    ProjectileVisual.transform.SetParent(transform);
                    damage = ProjectileScriptableSO[4].damage;
                    projectileSpeed = ProjectileScriptableSO[4].projectileSpeed;
                    ProjectileVfx = ProjectileScriptableSO[4].ProjectileVfx;
                    break;
                case BulletType.Enemy2:
                    ProjectileVisual = Instantiate(ProjectileScriptableSO[5].ProjectileVisual);
                    ProjectileVisual.transform.position = transform.position;
                    ProjectileVisual.transform.SetParent(transform);
                    damage = ProjectileScriptableSO[5].damage;
                    projectileSpeed = ProjectileScriptableSO[5].projectileSpeed;
                    ProjectileVfx = ProjectileScriptableSO[5].ProjectileVfx;
                    break;
                case BulletType.Enemy3:
                    ProjectileVisual = Instantiate(ProjectileScriptableSO[6].ProjectileVisual);
                    ProjectileVisual.transform.position = transform.position;
                    ProjectileVisual.transform.SetParent(transform);
                    damage = ProjectileScriptableSO[6].damage;
                    projectileSpeed = ProjectileScriptableSO[6].projectileSpeed;
                    ProjectileVfx = ProjectileScriptableSO[6].ProjectileVfx;
                    break;
                case BulletType.Enemy4:
                    ProjectileVisual = Instantiate(ProjectileScriptableSO[7].ProjectileVisual);
                    ProjectileVisual.transform.position = transform.position;
                    ProjectileVisual.transform.SetParent(transform);
                    damage = ProjectileScriptableSO[7].damage;
                    projectileSpeed = ProjectileScriptableSO[7].projectileSpeed;
                    ProjectileVfx = ProjectileScriptableSO[7].ProjectileVfx;
                    break;
                case BulletType.Enemy5:
                    ProjectileVisual = Instantiate(ProjectileScriptableSO[8].ProjectileVisual);
                    ProjectileVisual.transform.position = transform.position;
                    ProjectileVisual.transform.SetParent(transform);
                    damage = ProjectileScriptableSO[8].damage;
                    projectileSpeed = ProjectileScriptableSO[8].projectileSpeed;
                    ProjectileVfx = ProjectileScriptableSO[8].ProjectileVfx;
                    break;
                case BulletType.Enemy6:
                    ProjectileVisual = Instantiate(ProjectileScriptableSO[9].ProjectileVisual);
                    ProjectileVisual.transform.position = transform.position;
                    ProjectileVisual.transform.SetParent(transform);
                    damage = ProjectileScriptableSO[9].damage;
                    projectileSpeed = ProjectileScriptableSO[9].projectileSpeed;
                    ProjectileVfx = ProjectileScriptableSO[9].ProjectileVfx;
                    break;
                case BulletType.Enemy7:
                    ProjectileVisual = Instantiate(ProjectileScriptableSO[10].ProjectileVisual);
                    ProjectileVisual.transform.position = transform.position;
                    ProjectileVisual.transform.SetParent(transform);
                    damage = ProjectileScriptableSO[10].damage;
                    projectileSpeed = ProjectileScriptableSO[10].projectileSpeed;
                    ProjectileVfx = ProjectileScriptableSO[10].ProjectileVfx;
                    break;
                case BulletType.Enemy8:
                    ProjectileVisual = Instantiate(ProjectileScriptableSO[11].ProjectileVisual);
                    ProjectileVisual.transform.position = transform.position;
                    ProjectileVisual.transform.SetParent(transform);
                    damage = ProjectileScriptableSO[11].damage;
                    projectileSpeed = ProjectileScriptableSO[11].projectileSpeed;
                    ProjectileVfx = ProjectileScriptableSO[11].ProjectileVfx;
                    break;
                case BulletType.Enemy9:
                    ProjectileVisual = Instantiate(ProjectileScriptableSO[12].ProjectileVisual);
                    ProjectileVisual.transform.position = transform.position;
                    ProjectileVisual.transform.SetParent(transform);
                    damage = ProjectileScriptableSO[12].damage;
                    projectileSpeed = ProjectileScriptableSO[12].projectileSpeed;
                    ProjectileVfx = ProjectileScriptableSO[12].ProjectileVfx;
                    break;
                

            }
        
        

    }

    public virtual void Launch() { 
      transform.Translate(direction*projectileSpeed);
    }

    public virtual void MoveTowardstarget(Transform target) {
        //Vector3.MoveTowards(transform.position,target.position,1);
        //transform.Translate(Vector3.forward*projectileSpeed*2*Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, target.position + new Vector3(0,1,0), projectileSpeed * Time.deltaTime *2);
        ProjectileVisual.transform.Rotate(Vector3.up * projectileSpeed*Time.timeScale);
        
       transform.LookAt(target);
       onbombExplode?.Invoke();
    }
   
    public virtual void OnDestroyObj()
    {
        Instantiate(ProjectileVfx,transform.position, Quaternion.identity);
    }

}
