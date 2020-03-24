using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    GameObject canvas;
    bool healthEnabled = false;

    void Start()
    {
        GameObject healthCanvasPrefab = (GameObject)Resources.Load("EnemyHealthCanvas");
        canvas = Instantiate(healthCanvasPrefab);
        canvas.transform.parent = gameObject.transform;
    }
    public void UpdateHealthBar(float health,float maxHealth )
    {
        if(healthEnabled == false)
            EnableHealthUI();
        
        float res =((float)health /maxHealth);
       
        foreach (Transform child in canvas.transform)
        {
            child.GetComponent<Image>().fillAmount = res;
        }
    }
    void EnableHealthUI()
    {
        foreach (Transform child in canvas.transform)
        {
            child.GetComponent<Image>().enabled = true;
        }
    }
}
