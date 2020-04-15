using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityAdsManager : MonoBehaviour, IUnityAdsListener
{
    //public static UnityAdsManager instance;
    [SerializeField]string gameId = "1234567";
    [SerializeField]string videoId = "video";
    [SerializeField]string rewardedVideoId = "rewardedVideo";
    [SerializeField]bool testMode = true;

    [SerializeField]SurvivalGameManager gameManager;
    [SerializeField] SurvivalGameUI gameUI;
    [SerializeField]Button myButton;
    [SerializeField] TextMeshProUGUI x2Text;

    /* void Awake()
    {
        if (UnityAdsManager.instance == null)
        {
            UnityAdsManager.instance = this;
        }
        else if (UnityAdsManager.instance != null)
        {
            Destroy(UnityAdsManager.instance.gameObject);
            UnityAdsManager.instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    } */

    void Start () {   
        // Set interactivity to be dependent on the Placement’s status:
        myButton.interactable = Advertisement.IsReady (rewardedVideoId); 
        gameUI.canShowAd = Advertisement.IsReady (videoId); 
        // Map the ShowRewardedVideo function to the button’s click listener:
        if (myButton) myButton.onClick.AddListener (ShowRewardedVideo);
        // Initialize the Ads listener and service:
        Advertisement.AddListener (this);
        Advertisement.Initialize (gameId, true);
    }
    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo () {
        Advertisement.Show (rewardedVideoId);
    }
    public void ShowRegularAd () {
        Advertisement.Show (videoId);
    }
    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady (string placementId) {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == rewardedVideoId) {        
            myButton.interactable = true;
        }
        else if (placementId == videoId )
        {
            gameUI.canShowAd = true;
        }
    }

    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        if(placementId == rewardedVideoId)
        {
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
        else  if(placementId == videoId)
        {
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
    }

    public void OnUnityAdsDidError (string message) {
        Debug.LogWarning (message);
    }

    public void OnUnityAdsDidStart (string placementId) {
        // Optional actions to take when the end-users triggers an ad.
    }
    void OnDestroy()
    {
        Debug.Log("DestroyAdController");
        myButton.onClick.RemoveListener(ShowRewardedVideo);
        Advertisement.RemoveListener(this);
    }
}
