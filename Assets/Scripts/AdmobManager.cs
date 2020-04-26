using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using TMPro;
public class AdmobManager : MonoBehaviour
{
    [SerializeField] string App_ID = "ca-app-pub-5176018929650163~8582334198";
    [SerializeField] string rewardedAd_ID = "ca-app-pub-3940256099942544/5224354917";
    [SerializeField] string InterstitialAd_Id = "ca-app-pub-3940256099942544/1033173712";

    [SerializeField] Button rewardedVideoButton;
    [SerializeField] TextMeshProUGUI x2Text;
    [SerializeField] SurvivalGameManager manager;
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;
    bool isRewarded,isAdClosed;
    void Start()
    {
        MobileAds.Initialize(App_ID);
        RequestRewardBasedVideo();
        if(SaveAndLoadGameData.instance.savedData.playedGameCount%2 != 0 
            && SaveAndLoadGameData.instance.savedData.isAdsRemoved == false)
        {
            FindObjectOfType<AdmobManager>().RequestInterstitial();
        }
    }

    public void RequestInterstitial()
    {
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(InterstitialAd_Id);

        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded()) {
            this.interstitial.Show();
        }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (isAdClosed)
        {
            if (isRewarded)
            {
                manager.GainCoin(manager.GetCoinGained());
                x2Text.gameObject.SetActive(true);
                rewardedVideoButton.gameObject.SetActive(false);
                Debug.Log("rewarded");
                // do all the actions
                // reward the player
                isRewarded = false;
            }
            else
            {
                // Ad closed but user skipped ads, so no reward 
            // Ad your action here 
            }
            isAdClosed = false;  // to make sure this action will happen only once.
        }
        
    }
   
    public void RequestRewardBasedVideo()
    {
        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, rewardedAd_ID);
    }

    public void ShowRewardedVideoAd()
    {
         if (rewardBasedVideo.IsLoaded()) {
            rewardBasedVideo.Show();
        }
        else{
            Debug.Log("Interstitial Ad is not ready and couldnt Show it");
        }
    }
    //Events
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }
    //EVENTS AD DELEGATES FOR REWARD BASED VIDEO
     public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        print("HandleRewardBasedVideoClosed event received");

        RequestRewardBasedVideo(); // to load Next Videoad
        isAdClosed = true;
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        print("HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " +
                type);
        isRewarded = true;;
    }
}
