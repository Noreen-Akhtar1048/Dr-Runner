using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PickableItems")]
public class PickableSO : ScriptableObject
{
    public GameObject Visual;
    public int price;
    public Vector3 Rotation;
    public Vector3 locakSale;

    public enum pickableType
    {
        Akm, Vector, ShotGun, Rocket
    }
    public pickableType type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
