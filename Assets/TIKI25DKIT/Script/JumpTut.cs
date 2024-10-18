using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTut : MonoBehaviour
{
    
    public GameObject tuttorialpanel;
    public GameObject jumpArrow;
    public GameObject mainControl;
    
    public GameObject[] Buttons;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt("JumpTut");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
       
        
        if (other.gameObject.CompareTag("Player"))
        {
            if ( PlayerPrefs.GetInt("JumpTut")==0)
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
               
               mainControl.SetActive(false);
               Time.timeScale = 0;
                tuttorialpanel.SetActive(true);
                jumpArrow.SetActive(true);
                // Destroy(this.gameObject);
                PlayerPrefs.SetInt("JumpTut", 1);
               
            }
            
        
        }
    }
}
