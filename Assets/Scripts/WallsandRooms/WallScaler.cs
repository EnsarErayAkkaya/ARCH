using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScaler : MonoBehaviour
{
    public float scaleSpeed,scaleDuration;
    public Vector2 minScale;
    SurvivalGameManager survivalManager;
    void Start()
    {
        survivalManager = FindObjectOfType<SurvivalGameManager>();
    }

    public void CallScaler()
    {
        StartCoroutine( ScaleWallDown() );
    }
    IEnumerator ScaleWallDown()
    {
        Vector3 Scale = gameObject.transform.localScale;
        float t = 0.0f;
        while(t < scaleDuration)
        {
            if(survivalManager.gameEnded == true)
                break;
            
            if(survivalManager.gameStopped == false )
            {
                t += Time.deltaTime / Time.timeScale / scaleDuration;
                gameObject.transform.localScale = Vector3.Lerp(Scale, minScale, t);
            }
            
            yield return null;
        }
    }
}
