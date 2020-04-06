using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image healthUI;
    public void UpdateHealthBar(float health)
    {
        float res =((float)health /100);
        healthUI.fillAmount = res;
    }
   
}
