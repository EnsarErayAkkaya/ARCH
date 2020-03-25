using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    bool healthEnabled = false;
    [SerializeField]
    GameObject healthbar,healthBack;
    
    public void UpdateHealthBar(float health,float maxHealth )
    {
        if(healthEnabled == false)
            EnableHealthUI();

        float res =((float)health /maxHealth);
        healthbar.GetComponent<Image>().fillAmount = res;
    }
    void EnableHealthUI()
    {
        healthEnabled = true;
        healthBack.SetActive(true);
    }
}
