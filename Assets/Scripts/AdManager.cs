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
	
    public void StartAd()
    {
		gameManager = GameManager.instance;
		uiController = GameObject.Find("UI Controller").GetComponent<UIController>();
		
		string adUnitId;
        adUnitId = "ca-app-pub-2964040886574646/5411926434";
		// For Test:	adUnitId = "ca-app-pub-3940256099942544/5224354917";

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
		if (failedToLoad)
		{
			FailedToLoad();
		}
		
		if (this.rewardedAd.IsLoaded())
		{
			this.rewardedAd.Show();
		}
		
		else
		{
			StartCoroutine(ButtonWasPressed());
		}
	}
	
	IEnumerator ButtonWasPressed()
	{
		yield return new WaitForSeconds(.2f);
		ShowAd();
	}
	
	IEnumerator FailedToLoad()
	{
		yield return new WaitForSeconds(1f);
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
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdClosed event received");
		gameManager.SetLives(3);
		uiController.ShowWeAreBackPanel();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = "Lives";
        double amount = 3;
        MonoBehaviour.print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);
    }
	
}