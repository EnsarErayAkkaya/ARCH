using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsManager : MonoBehaviour
{
    public string gameId = "1234567";
    public string rewardedVideoId = "rewardedVideo";
    public bool testMode = true;
    public static UnityAdsManager instance;
    void Awake()
    {
		if(instance != null)
		{
			Debug.LogWarning("More than one instance of AdsManager found");
			return;
		}
		instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
