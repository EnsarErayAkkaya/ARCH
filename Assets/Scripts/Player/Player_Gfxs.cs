using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Gfxs : MonoBehaviour
{
    public SpriteRenderer energyGlass;
    Player_Shoot player_Shoot;
    public Color normal,middle,powerfull,startColor;
    SpriteRenderer energySprite;
    void Start()
    {
        player_Shoot = GetComponent<Player_Shoot>();
        energySprite =  energyGlass.GetComponent<SpriteRenderer>();
    }
    public void CallSetEnergyGlass()
    {
        StartCoroutine( SetEnergyGlass() );
    }
    IEnumerator SetEnergyGlass()
    {
        float t = 0.0f;
        while(t < player_Shoot.PowerfulShootTimeLimit + 2f)
        {
            if(player_Shoot.ShootCharging == false)
                break;
            
            t += Time.deltaTime * (Time.timeScale / 1);
            if( t <= player_Shoot.MiddleShootTimeLimit)
            {
                energySprite.color 
                    = Color.Lerp(energySprite.color, normal, t);
            }
            else if(t > player_Shoot.MiddleShootTimeLimit && t <= player_Shoot.PowerfulShootTimeLimit)
            {
                energySprite.color 
                    = Color.Lerp(energySprite.color, middle, t);
            }
            else if(t > player_Shoot.PowerfulShootTimeLimit && t <= player_Shoot.PowerfulShootTimeLimit + 2)
            {
                energySprite.color 
                    = Color.Lerp(energySprite.color, powerfull, t);
            }
            yield return 0;
        }
        CallSetEnergyGlassToNormal();
    }
    public void CallSetEnergyGlassToNormal()
    {
        StartCoroutine( SetEnergyGlassToNormal() );
    }
    IEnumerator SetEnergyGlassToNormal()
    {
        float t = 0.0f;
        while(t < 1.0f)
        {
            if(player_Shoot.ShootCharging == true)
                break;

            t += Time.deltaTime * (Time.timeScale / 1);
            energySprite.color 
                = Color.Lerp(energySprite.color, startColor, t);
            yield return 0;
        }
    }
}
