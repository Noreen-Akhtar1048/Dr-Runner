using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Linq;
//using Unity.PlasticSCM.Editor.WebApi;
using System.Numerics;
[RequireComponent(typeof(BoxCollider))]
public class Rocket : projectile
{

    public List<GameObject> targets=new List<GameObject>();
    public Transform currenttarget;
    public float startPoint;
    public float launchradius, blastradius;
    bool canlanch;
    public float launchTime;
    bool isLaunched;
    
    public override void Launch()
    {
        transform.Translate(UnityEngine.Vector3.forward * projectileSpeed*Time.deltaTime);
        canlanch = true;

    }

    void moveUp()
    {
      //  transform.Translate()
    }
    // Start is called before the first frame update
    public override void Start()
    {
        // subscribe to uimanagers launch button
        base.Start();
      
        MenuManager.Instance.Rocketbtn.onClick.AddListener(SetTarget);
    }
   

    // Update is called once per frame
    void Update()
    {
        if (canlanch)
        {
            if (launchTime >= 0)
            {
                launchTime -= Time.deltaTime;
                Launch();
            }
            else
            {
                MoveTowardstarget(currenttarget);
            }
        }
       
    }
    private void OnDrawGizmos()
    {
     Gizmos.color = new Color(1, 0, 0, 0.15f);
        Gizmos.DrawSphere(transform.position, launchradius);
        
    }
    [ContextMenu("settarget")]
    public   void SetTarget()
    {
        transform.parent = null;
        targets.Clear();
        Collider[] probableTargets = Physics.OverlapSphere(transform.position, launchradius);
        if (probableTargets.Length > 0)
        {
            foreach (Collider target in probableTargets)
            {
                
                    if (target.GetComponent<target>()!=null)
                        targets.Add(target.gameObject);
            }

            print(targets.Count);
            currenttarget = FindNearestGameObject();
            print(currenttarget?.name);
            Launch();
           
           
        }
        else
        {
            Destroy(gameObject);
        }
    }

    Transform FindNearestGameObject()
    {
        if (targets == null  || targets.Count == 0)
        {
            return null;
        }
        GameObject nearestObject = targets
            .OrderBy(go => UnityEngine.Vector3.Distance(go.transform.position, gameObject.transform.position))
            .LastOrDefault();

        return nearestObject.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canlanch)
        {
            Collider[] objects = Physics.OverlapSphere(transform.position, blastradius);
            foreach (Collider obj in objects)
            {
                if (obj.TryGetComponent<IDamageAble>(out IDamageAble damagable))
                {
                    damagable.Damage(damage);
                    base.OnDestroyObj();
                }
            }

            
            Destroy(gameObject);
        }
    }
}
