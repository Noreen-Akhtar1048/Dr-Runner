using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Guns")]
public class GunSO : ScriptableObject
{
    public int GunId;
    public float FireRate;
    //public int price;
    public float damage;
    public float magzineCapacity;
    public GameObject MuzzleVfx;
  
    
}
