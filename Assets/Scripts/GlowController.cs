using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MK.Glow.Legacy;
using TMPro;

public class GlowController : MonoBehaviour
{
    public MKGlowFree glowFree;
    public string performance, graphics;
    public TextMeshProUGUI buttonText;
    bool glow;
    void Awake()
    {
        glow = SaveAndLoadGameData.instance.savedData.isGlow;
        glowFree.enabled = glow;
        changeText();
    }
    public void ChangeGlow()
    {
        if(glow)
        {
            glow = false;
        }
        else{
            glow = true;
        }
        glowFree.enabled = glow;

        changeText();
        SaveGlow();
    }
    void SaveGlow()
    {
        SaveAndLoadGameData.instance.savedData.isGlow = glow;
        SaveAndLoadGameData.instance.Save();
    }
    void changeText()
    {
        if(glow == true)
        {
            buttonText.text = performance;
        }
        else
        {
            buttonText.text = graphics;
        }
    }
}
