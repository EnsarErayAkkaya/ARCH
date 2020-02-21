using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour
{
    public bool shake;
    public void Call_Shake(float duration, float magnitude)
    {
        if(shake)
        {
            StartCoroutine(Shake(duration,magnitude));
        }
    }
    IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;
        while( elapsed < duration )
        {
            float x = Random.Range(-1f,1f) * magnitude;
            float y = Random.Range(-1f,1f) * magnitude;

            transform.position =  new Vector3(transform.position.x +x, transform.position.y+y,transform.position.z);
            
            elapsed += Time.deltaTime;
            yield return null; 
        }
    }
}
