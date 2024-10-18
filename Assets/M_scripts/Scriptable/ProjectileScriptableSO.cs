using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Projectile")]
public class ProjectileScriptableSO : ScriptableObject
{

    public GameObject ProjectileVisual;
    public float damage;
    public float projectileSpeed;
    public GameObject ProjectileVfx;
    public Vector3 direction;
    public Vector3 VisualRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
