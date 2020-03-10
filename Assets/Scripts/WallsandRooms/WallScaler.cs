using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScaler : MonoBehaviour
{
    public float scaleSpeed,scaleDuration;
    public Vector2 minScale;
    SurvivalGameManager survivalManager;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
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
            if(survivalManager.gameStopped == true )
                break;
            t += Time.deltaTime / Time.timeScale / scaleDuration;
            gameObject.transform.localScale = Vector3.Lerp(Scale, minScale, t);
            yield return null;
        }
    }
}
