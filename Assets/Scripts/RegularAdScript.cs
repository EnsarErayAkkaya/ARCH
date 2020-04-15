using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RegularAdScript : MonoBehaviour,IUnityAdsListener
{
  
    [SerializeField]string gameId = "1234567";
    [SerializeField]string myPlacementId = "rewardedVideo";
    [SerializeField]bool testMode = true;
    [SerializeField]SurvivalGameManager gameManager;
    [SerializeField] SurvivalGameUI gameUI;
    void Start () {   
        gameUI.canShowAd = Advertisement.IsReady (myPlacementId); 
        // Initialize the Ads listener and service:
        Advertisement.AddListener (this);
        Advertisement.Initialize (gameId, true);
    }

    // Implement a function for showing a rewarded video ad:
    public void ShowRegularAd () {
        Advertisement.Show (myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady (string placementId) {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId )
        {
            gameUI.canShowAd = true;
        }
    }

    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
        } 
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Add skipped");
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning ("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError (string message) {
        Debug.LogWarning (message);
    }

    public void OnUnityAdsDidStart (string placementId) {
        // Optional actions to take when the end-users triggers an ad.
    } 
}
