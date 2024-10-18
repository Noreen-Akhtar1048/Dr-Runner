using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class splash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("showAppOpen", 6f);
        
        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        
        yield return new WaitForSeconds(10f);
        
        SceneManager.LoadScene(1);
        
        PlayerPrefs.SetInt("RemoveAdOpen", 0);
 
    }

    [ContextMenu("showAppOpen")]
    public void showAppOpen()
    {
        GoogleAdMobController.Instance?.ShowAppOpenAd();
        GoogleAdMobController.Instance.HideBanner();
        print("AppOpenAdShow");
    }
}
