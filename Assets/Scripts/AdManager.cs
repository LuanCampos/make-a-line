using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class AdManager : MonoBehaviour
{
    private RewardedAd rewardedAd;
	private UIController uiController;
	private GameManager gameManager;
	private bool failedToLoad = false;
	private bool showAdButton = false;
	
	void Update()
	{
		if (showAdButton == true)
		{
			if (failedToLoad)
			{
				showAdButton = false;
				StartCoroutine(NoConnection());
			}
		
			if (this.rewardedAd.IsLoaded())
			{
				showAdButton = false;
				this.rewardedAd.Show();
			}
		}
	}
	
    public void StartAd()
    {
		gameManager = GameManager.instance;
		uiController = GameObject.Find("UI Controller").GetComponent<UIController>();
		
		string adUnitId;
        #if UNITY_ANDROID
            // adUnitId = "ca-app-pub-2964040886574646/5411926434"; // Mine
			adUnitId = "ca-app-pub-3940256099942544/5224354917"; // Test
        #elif UNITY_IPHONE
            // adUnitId = "ca-app-pub-2964040886574646/4767415837"; // Mine
			adUnitId = "ca-app-pub-3940256099942544/1712485313"; // Test
        #else
            adUnitId = "unexpected_platform";
        #endif

        this.rewardedAd = new RewardedAd(adUnitId);
		
        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
		
		LoadAd();
    }
	
	public void LoadAd()
    {
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }
	
	public void ShowAd()
	{
		showAdButton = true;
	}
	
	IEnumerator NoConnection()
    {
        yield return new WaitForSecondsRealtime(1f);
        uiController.ShowNoConnectionPanel();
    }
	
	public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdFailedToLoad event received with message: " + args.Message);
		failedToLoad = true;
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdFailedToShow event received with message: " + args.Message);
		uiController.ShowNoConnectionPanel();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdClosed event received");
		gameManager.SetLives(1);
		uiController.ShowWeAreBackPanel();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = "Lives";
        double amount = 3;
        MonoBehaviour.print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);
		gameManager.SetLives(2);
    }
	
}