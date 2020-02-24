using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandomWalls : MonoBehaviour
{
    private float reflectorWallMinXScale=.35f,reflectorWallMaxXScale=1.4f
            ,reflectorWallMinYScale=.37f,reflectorWallMaxYScale=1.45f;

    private float radius;
    public int wallCount;
    public List<GameObject> walls;
    SurvivalGameManager survivalManager;
    void Start()
    {   
        survivalManager = FindObjectOfType<SurvivalGameManager>();
    }
    public void CreateWalls()
    {
        radius = FindObjectOfType<SurvivalGameManager>().gameRadius;
        
        ChooseWallCount();   
        
        for (int i = 0; i < wallCount; i++)
        {
            int k = ChooseWallType();
            GameObject wall = Instantiate( walls[k], ChooseRandomLocation(), Quaternion.identity );
            
            Color c =  Random.ColorHSV(0,1,1,1,1,1);
            if(wall.CompareTag("Wall"))
            {
                if( !wall.GetComponent<GlassScript>())
                {
                    wall.GetComponent<SpriteRenderer>().material.SetColor("_EmissionColor", c);
                }
                wall.transform.localScale = new Vector3(Random.Range(reflectorWallMinXScale,reflectorWallMaxXScale)
                        ,Random.Range(reflectorWallMinYScale,reflectorWallMaxYScale),transform.localScale.z);
            }
               
        }
    }

    int ChooseWallType()
    {
        float val = Random.Range(0f,1f);
        if(val <= .4f)
        {
            return 0;
        }
        else if( val > .4f && val <= .6f)
        {
            return 1; 
        }
        else if(val > .6f && val <= 1 )
        {
            return 2;
        }
        else{
            return 0;
        }
    }

    void ChooseWallCount()
    {
        if( survivalManager.waveIndex < 5 && radius <= 60 )
        {
            wallCount = Random.Range(4,9);
        }
        else if( survivalManager.waveIndex < 5 && radius > 60 )
        {
            wallCount = Random.Range(6,13);
        }
        else if( survivalManager.waveIndex > 5 && survivalManager.waveIndex <10 && radius <= 60 )
        {
            wallCount = Random.Range(6,11);
        }
        else if( survivalManager.waveIndex > 5 && survivalManager.waveIndex <10 && radius > 60 )
        {
            wallCount = Random.Range(8,15);
        }
        else if( survivalManager.waveIndex > 15 && radius <= 60 )
        {
            wallCount = Random.Range(7,12);
        }
        else if( survivalManager.waveIndex > 15 && radius > 60 )
        {
            wallCount = Random.Range(9,16);
        }
    }
    Vector2 ChooseRandomLocation()
    {
        var vector2 = Random.insideUnitCircle * radius;
        return new Vector2(transform.position.x + vector2.x,transform.position.y + vector2.y);
    }
}
