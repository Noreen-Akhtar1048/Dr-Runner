using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTut : MonoBehaviour
{
    public GameObject tuttorialpanel;
    public GameObject punchArrow;
    public GameObject mainControl;
    
    public GameObject[] Buttons;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt("PunchTut");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            if ( PlayerPrefs.GetInt("PunchTut")==0)
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
                punchArrow.SetActive(true);
                // Destroy(this.gameObject);
                PlayerPrefs.SetInt("PunchTut", 1);
               
            }
            
        
        }
    }
}
