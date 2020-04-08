using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandomWalls : MonoBehaviour
{
    private float reflectorWallMinXScale=.35f,reflectorWallMaxXScale=1.4f
            ,reflectorWallMinYScale=.37f,reflectorWallMaxYScale=1.45f;

    private float radius;
    public int wallCount;
    public List<GameObject> wallprefabs;
    SurvivalGameManager survivalManager;

    [SerializeField] int Glass_levelStart, GlassWallWithBomb_levelStart;
    List<GameObject> wallsToCreate = new List<GameObject>(); 
    public Transform wallsParent;

    void Start()
    {   
        survivalManager = FindObjectOfType<SurvivalGameManager>();
    }
    public void CreateWalls()
    {
        radius = FindObjectOfType<SurvivalGameManager>().gameRadius;
        ChooseWallsToCreate();
        ChooseWallCount();   
        
        for (int i = 0; i < wallCount; i++)
        {
            int k = ChooseWallType();
            
            CreateAWall(k);
        }
    }
    ///<summary>
    ///This function can called when a spesific wall needed 
    ///to created.
    ///for normal wall give 0,
    ///for glass wall with bomb give 1,
    ///for normal glass give 2 .
    ///</summary>
    
    public void CreateAWall(int k)
    {
        GameObject wall = Instantiate( wallsToCreate[k], ChooseRandomLocation(), Quaternion.identity );
            wall.transform.SetParent(wallsParent.transform); 
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
    void ChooseWallsToCreate()
    {
        wallsToCreate.Clear();
        wallsToCreate.Add(wallprefabs[0]);
        // Glass wall adding
        if(survivalManager.waveIndex >= Glass_levelStart )
        {
            wallsToCreate.Add(wallprefabs[1]);
        }
        // Glass Wall with bomb adding
        if(survivalManager.waveIndex >= GlassWallWithBomb_levelStart)
        {
            wallsToCreate.Add(wallprefabs[2]);
        }
    }
    int ChooseWallType()
    {
        float val = Random.Range(0f,1f);
        if(wallsToCreate.Count == 1)
        {
            return 0;
        }
        else if (wallsToCreate.Count == 2)
        {
            if(val <= .7f)
            {
                return 0;
            }
            else if( val > .7f && val <= .1f)
            {
                return 1; 
            }
        }
        else if(wallsToCreate.Count == 3)
        {
            if(val <= .5f)
            {
                return 0;
            }
            else if( val > .5f && val <= .8f)
            {
                return 1; 
            }
            else if(val > .8f && val <= 1 )
            {
                return 2;
            }
        }
        return 0;
        
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
