using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParticles_Follow : MonoBehaviour
{
    public Transform target;
    public float SmoothSpeed = 0.125f;
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(target.position.x,target.position.y,transform.position.z),SmoothSpeed * Time.deltaTime);
    }
}
