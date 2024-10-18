using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;
    public float shakeDuration =0;
    public float setduration =0;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;
    public float elapsed = 0f;

    private void OnEnable()
    {
        SimpleProjectile.onbombExplode += SetShakeTime;
        projectile.onbombExplode += SetShakeTime;
    }
    private void OnDisable()
    {
        SimpleProjectile.onbombExplode -= SetShakeTime;
        projectile.onbombExplode -= SetShakeTime;

    }

    void SetShakeTime()
    {
        print("i am shaking");
        elapsed = 0f;
        shakeDuration =setduration ;
    }

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent<Transform>();
        }

        //originalPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        if (elapsed < shakeDuration)
        {
            Vector3 newPos = transform.position + Random.insideUnitSphere * shakeMagnitude;
            cameraTransform.localPosition = newPos;

            elapsed += Time.deltaTime;
        }
        else
        {
            return;
        }
    }
}
