using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6 : Enemy
{
    bool canShoot;

    private void OnEnable()
    {
        SimpleEnemy.OnShoot += SimpleEnemy_OnShoot;
    }

    private void OnDisable()
    {
        SimpleEnemy.OnShoot -= SimpleEnemy_OnShoot;
    }
    private void SimpleEnemy_OnShoot(object sender, GameObject e)
    {
        e.GetComponent<Enemy6>()?.Shoot();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            base.Shoot();
            SetBulletType();
        }
    }
    [ContextMenu("settype")]
    async void SetBulletType()
    {
        await System.Threading.Tasks.Task.Delay(80);
        base.projectileToIntantiate.GetComponent<projectile>().type = (projectile.BulletType)9;
        canShoot = true;
        print("set");
    }
}
