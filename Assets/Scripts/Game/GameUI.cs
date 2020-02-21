using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image healthLeft,healthRight;
    public void UpdateHealthBar(int health)
    {
        float res =((float)health /100);
        healthLeft.fillAmount = res;
        healthRight.fillAmount = res;
    }
   
}
