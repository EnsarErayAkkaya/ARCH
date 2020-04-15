using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using TMPro;
public class RewardedAdsScript : MonoBehaviour, IUnityAdsListener { 

    [SerializeField]string gameId = "1234567";
    [SerializeField]string myPlacementId = "rewardedVideo";
    [SerializeField]bool testMode = true;

    [SerializeField] TextMeshProUGUI x2Text;
    [SerializeField]Button myButton;
    [SerializeField]SurvivalGameManager gameManager;
    [SerializeField] SurvivalGameUI gameUI;
    void Start () {   
        // Set interactivity to be dependent on the Placement’s status:
        myButton.interactable = Advertisement.IsReady (myPlacementId); 

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (myButton) myButton.onClick.AddListener (ShowRewardedVideo);
        // Initialize the Ads listener and service:
        Advertisement.AddListener (this);
        Advertisement.Initialize (gameId, true);
    }

    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo () {
        Advertisement.Show (myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady (string placementId) {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId) {        
            myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            // Kazandığın para kadar tekrar kazan. 
            gameManager.GainCoin( gameManager.GetCoinGained() );
            x2Text.gameObject.SetActive(true);
            Debug.Log("Add watched");
            // Bir kere kullanıldıktan sonra tekrar kullanılamaz.
            myButton.gameObject.SetActive(false);
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