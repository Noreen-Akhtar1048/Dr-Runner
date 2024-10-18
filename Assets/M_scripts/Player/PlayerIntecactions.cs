using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIntecactions : MonoBehaviour
{
    public static event EventHandler<IPickable> OnPickedEvent;
    public static event EventHandler<ICollectable> OnCollectedEvent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IPickable>(out IPickable pickable)) {
           
            OnPickedEvent?.Invoke(this, pickable);
            Destroy(other.gameObject);
        }
        if (other.TryGetComponent<ICollectable>(out ICollectable collected))
        {
            OnCollectedEvent?.Invoke(this, collected);
            Destroy(other.gameObject);
        }
    }
}
