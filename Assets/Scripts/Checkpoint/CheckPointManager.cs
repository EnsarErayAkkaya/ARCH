using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public List<GameObject> checkPointsPrefabList = new List<GameObject>();
    private float radius;
    public int checkPointCount;
    public List<GameObject> checkPoints = new List<GameObject>();
    SurvivalGameManager survivalManager;
    [SerializeField] int checkPointWithGlass_StartLevel;
    void Start()
    {
        survivalManager = FindObjectOfType<SurvivalGameManager>();
    }

    public void CreateCheckPoints()
    {
        radius = FindObjectOfType<SurvivalGameManager>().gameRadius-2;

        ChoosecheckPointCount();
        List<GameObject> checks = CheckpointsToCreate();
        for (int i = 0; i < checkPointCount; i++)
        {
            GameObject check = Instantiate( checks[ChooseCheckpointType()], ChooseRandomLocation(), Quaternion.identity );
            
            Color c =  Random.ColorHSV(0,1,1,1,1,1);
            foreach (Transform item in check.transform)
            {
                if(item.GetComponent<SpriteRenderer>() != null && !item.CompareTag("Wall"))
                    item.GetComponent<SpriteRenderer>().material.SetColor("_EmissionColor", c);
                if(item.GetComponent<ParticleSystem>() != null)
                {
                    var col = item.GetComponent<ParticleSystem>().colorOverLifetime;
                    col.enabled = true;
                    Gradient grad = new Gradient();
                    grad.SetKeys( new GradientColorKey[] { 
                        new GradientColorKey(c, 0.0f), new GradientColorKey(c, 1.0f) 
                        },
                        new GradientAlphaKey[] { 
                            new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) 
                            } );
                    col.color = grad;
                }
            }
            checkPoints.Add(check);
        }
    }

    List<GameObject> CheckpointsToCreate()
    {
        List<GameObject> checks = new List<GameObject>();

        checks.Add(checkPointsPrefabList[0]);

        if(survivalManager.waveIndex >= checkPointWithGlass_StartLevel)
        {
            checks.Add(checkPointsPrefabList[1]);
        }

        return checks;
    }
    int ChooseCheckpointType()
    {
        if(survivalManager.waveIndex < 8)
            return 0;
        else
        {
            float val = Random.Range(0f,1f);
            if(val <= .7f)
            {
                return 0;
            }
            else if( val > .7f && val <= 1f)
            {
                return 1; 
            }
            else{
                return 0;
            }
        }
    }
    
    public void ChoosecheckPointCount()
    {
        if( survivalManager.waveIndex < 5 && radius <= 60 )
        {
            checkPointCount = UnityEngine.Random.Range(2,5);
        }
        else if( survivalManager.waveIndex < 5 && radius > 60 )
        {
            checkPointCount = UnityEngine.Random.Range(3,7);
        }
        else if( survivalManager.waveIndex > 5 && survivalManager.waveIndex <10 && radius <= 60 )
        {
            checkPointCount = UnityEngine.Random.Range(3,7);
        }
        else if( survivalManager.waveIndex > 5 && survivalManager.waveIndex <10 && radius > 60 )
        {
            checkPointCount = UnityEngine.Random.Range(6,10);
        }
        else if( survivalManager.waveIndex > 15 && radius <= 60 )
        {
            checkPointCount = UnityEngine.Random.Range(9,13);
        }
        else if( survivalManager.waveIndex > 15 && radius > 60 )
        {
            checkPointCount = UnityEngine.Random.Range(12,16);
        }
    }
    Vector2 ChooseRandomLocation()
    {
        var vector2 = Random.insideUnitCircle * radius;
        return new Vector2(transform.position.x + vector2.x,transform.position.y + vector2.y);
    }
    public void RemovePointFromlist(GameObject point)
    {
        checkPoints.Remove(point);
        FindObjectOfType<SurvivalGameManager>().GetEnemyScore();
        if(checkPoints.Count == 0)
        {
            Debug.Log("All checkPoint Passed");
            FindObjectOfType<SurvivalGameManager>().CleanGame();
            FindObjectOfType<SurvivalGameUI>().SetUIOnGamePassed();
            FindObjectOfType<SurvivalGameManager>().CalculateScore();
        }
    }
}
