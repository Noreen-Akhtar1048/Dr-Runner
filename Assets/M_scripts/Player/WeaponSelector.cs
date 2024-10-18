using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    
    public Gun[] guns;
    public static int currentWeapon;
    
    public GameObject rocketPosition;
    public GameObject rocketPrefab;

    public ParticleSystem PurchasedText;

    private void OnEnable()
    {
        PlayerIntecactions.OnPickedEvent += PlayerIntecactions_OnPickedEvent;
    }

    private void OnDisable()
    {
        PlayerIntecactions.OnPickedEvent -= PlayerIntecactions_OnPickedEvent;

    }
    private void PlayerIntecactions_OnPickedEvent(object sender, IPickable e)
    {  
       currentWeapon=(int) e.pickedObj.GetComponent<Pickable>().type;
        if (GlobalValue.SavedNewCoins >= e.pickedObj.GetComponent<Pickable>().price)
        {
            GlobalValue.SavedNewCoins -= e.pickedObj.GetComponent<Pickable>().price;
            PurchasedText.Play();
            if (currentWeapon == 3)
            {
                print(currentWeapon);
                var rocketObj = Instantiate(rocketPrefab, rocketPosition.transform);
                rocketObj.transform.SetParent(rocketPosition.transform);
                // rocketObj.transform.position = Vector3.zero;
                return;
            }
          


            foreach (var g in guns)
            {
                g.gameObject.SetActive(g.gunid == currentWeapon);
                g.gameObject.GetComponent<Gun>().ResetMagzine();
            }
        }
        else
        {
            // activate purchace Ui panel
            MenuManager.Instance.ShowNECPanel();
            
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        PurchasedText.Stop();
        foreach (var g in guns)
        {

            g.gameObject.SetActive(false);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("increment")]
    public void SelectWeapon()
    {
        currentWeapon++;
        Debug.Log(currentWeapon);
    }
    public void Instantiate()
    {
       
    }
   
}
