using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class AKM : Gun
{

    public  override void Start()
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
        

        WeaponSelector.currentWeapon = 0;
        base.Shoot();
      SetBulletType();  
    }
    async void SetBulletType()
    {
        await System.Threading.Tasks.Task.Delay(100);
        base.projectileToIntantiate.GetComponent<projectile>().type = (projectile.BulletType)0;
    }
    public override void reaload()
    {
        base.reaload();
    }
}
    // Start is called before the first frame update
  

