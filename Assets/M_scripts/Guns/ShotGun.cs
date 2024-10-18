using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ShotGun : Gun
{
    // Start is called before the first frame update
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
    // Update is called once per frame
    [ContextMenu("Shoot")]

    public override void Shoot()
    {
        
        WeaponSelector.currentWeapon = 2;

        base.Shoot();
       SetBulletType();

    }
    async void SetBulletType()
    {
        await Task.Delay(100);
        base.projectileToIntantiate.GetComponent<projectile>().type = (projectile.BulletType)2;
    }
    public override void reaload()
    {
        base.reaload();
    }
}
