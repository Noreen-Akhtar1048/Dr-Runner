using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapTut : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tuttorialpanel;
    public GameObject doubleTapArrow;
    public GameObject mainControl;
    
    public GameObject[] Buttons;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt("DoubleTapTut");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("Player"))
        {
            if ( PlayerPrefs.GetInt("DoubleTapTut")==0)
            {
                for (int i = 0; i <= Buttons.Length; i++)
                {
                    Buttons[0].SetActive(true);
                    Buttons[1].SetActive(false);
                    Buttons[2].SetActive(false);
                    Buttons[3].SetActive(false);
                    Buttons[4].SetActive(false);
                    Buttons[5].SetActive(false);
                    Buttons[6].SetActive(false);
                   
                }
                print("Hello Boy");
                mainControl.SetActive(false);
                Time.timeScale = 0;
                tuttorialpanel.SetActive(true);
                doubleTapArrow.SetActive(true);
                // Destroy(this.gameObject);
                PlayerPrefs.SetInt("DoubleTapTut", 1);
               
            }
            
        
        }
    }
}
