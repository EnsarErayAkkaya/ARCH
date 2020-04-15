using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_XController : MonoBehaviour
{
    private Transform target;
    [SerializeField]Material transformedMaterial,normalMaterial;
    [SerializeField]float transformTime;
    [SerializeField]Enemy enemy;
    [SerializeField]SpriteRenderer sprite;
    
    void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        Look_Player();
    }

    void Look_Player()
    {
        var dir = target.position - transform.position;
        var angle =  Mathf.Atan2(dir.y,dir.x)* Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-90,Vector3.forward);
    }
    public void CallTransform()
    {
        StartCoroutine( Transform() );
    }
    IEnumerator Transform()
    {
        sprite.material = transformedMaterial;
        enemy.dontGetDamage = true;

        yield return new WaitForSeconds(transformTime);

        sprite.material = normalMaterial;
        enemy.dontGetDamage = false;
    }
}
