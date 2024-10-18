using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuttorialsScript : MonoBehaviour
{
    public bool stop=false;

    public GameObject tuttorialpanel;
    public GameObject bulletArrow,punchArrow;
    public GameObject mainControl;
    
    public GameObject[] Buttons;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt("EnemyTut");
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (PlayerPrefs.GetInt("EnemyTut")==0 )
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
                print("Player Enter");
                print(""+ stop);
                print("Throw Bullet");
                Time.timeScale = 0;
                tuttorialpanel.SetActive(true);
                bulletArrow.SetActive(true);
                punchArrow.SetActive(false);
               // Destroy(this.gameObject);
                PlayerPrefs.SetInt("EnemyTut", 1);
               
            }
            
        
        } 
       
    }

    
}
