using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour,IPickable
{
    public PickableSO[] pickableSO;
    public PickableSO currentSO;
    public int price;

    public enum pickableType
    {
        Akm,Vector,ShotGun,Rocket
    }
    public pickableType type;
    GameObject thisObject;
   
   
    public GameObject pickedObj
    {
        get { return thisObject; }
        set
        {
            if (thisObject == null)
            {
                thisObject = value;
            }
        }
    }

    public void Picked()
    {
        throw new NotImplementedException();
    }

   
    // Start is called before the first frame update
    void Start()
    {
        int index= UnityEngine.Random.Range(0, 4);
     //  print(index);
        currentSO = pickableSO[index];
        pickedObj = this.gameObject;
       MakePickable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MakePickable()
    {

        price = currentSO.price;
        // pickableSO.type = UnityEngine.Random.Range(0,3);
        type = (pickableType)currentSO.type;
        var visual=  Instantiate(currentSO.Visual);
        visual.transform.SetParent(gameObject.transform);
        visual.transform.localScale = currentSO.locakSale;
        visual.transform.localPosition = Vector3.zero;
        

    }
}
