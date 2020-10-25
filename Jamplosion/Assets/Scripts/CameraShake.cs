using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake()
    {
        Vector3 originalPos = transform.position;
        float shakeIntensity = 1f;
        while (shakeIntensity > 0f)
        {
            transform.position = originalPos + new Vector3(
                Random.Range(-shakeIntensity, shakeIntensity),    
                Random.Range(-shakeIntensity, shakeIntensity),    
                Random.Range(-shakeIntensity, shakeIntensity)    
            );
            shakeIntensity -= Time.deltaTime / 1f;
        yield return new WaitForEndOfFrame();
        }
        transform.position = originalPos;
    }
}
