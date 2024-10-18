using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : projectile
{
    bool canLaunch;

    public override void Start()
    {
        
        Launch();
        
    }
    public override void Launch()
    {
        canLaunch = true;
        transform.parent = null;
    }
    // Update is called once per frame
    void Update()
    {
        if (canLaunch)
        {
            base.Launch();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageAble>(out IDamageAble idamage))
        {
            idamage.Damage(damage);

        }
        OnDestroyObj();
    }
    public override void OnDestroyObj()
    {
        base.OnDestroyObj();
        Destroy(gameObject);
    }
}
