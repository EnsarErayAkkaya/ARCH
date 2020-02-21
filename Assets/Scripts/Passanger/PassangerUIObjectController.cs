using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassangerUIObjectController : MonoBehaviour
{
    public GameObject Image;
    public Sprite emptySeat,healthyPassanger,deadPassanger,normalPassanger,badPassanger;
    public void Set(PassangerHealth health)
    {
        var image = Image.GetComponent<Image>();
        if(health == PassangerHealth.good)
        {
            image.sprite = healthyPassanger;
        }
        else if(health == PassangerHealth.dead)
        {
            image.sprite = deadPassanger;
        }
        else if(health == PassangerHealth.bad)
        {
            image.sprite = badPassanger;
        }
        else if(health == PassangerHealth.normal)
        {
            image.sprite = normalPassanger;
        }
         else if(health == PassangerHealth.none)
        {
            image.sprite = emptySeat;
        }
    }
}
