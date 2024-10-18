using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Vector :Gun
// Start is called before the first frame update
{

    public override void Start()
    {
        base.Start();
    
    }

    private void OnEnable()
    {
        MenuManager.Instance.gunfirebtn.onClick.AddListener(Shoot);

    }

    private void OnDisable()
    {
        MenuManager.Instance.gunfirebtn.onClick.RemoveListener(Shoot);

    }
    [ContextMenu("Shoot")]
    public override void Shoot()
    {
        
        WeaponSelector.currentWeapon = 1;
       

        base.Shoot();
        SetBulletType();
    }
    async void SetBulletType()
    {
        await Task.Delay(110);
        base.projectileToIntantiate.GetComponent<projectile>().type = (projectile.BulletType)1;
    }
    public override void reaload()
    {
        base.reaload();
    }

}

