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
	private GameManager gameManager;
	private bool failedToLoad = false;
	private bool showAdButton = false;
	private bool badSignal = false;
	private string adUnitId;
	
	void Start()
	{
		StartCoroutine(WakeUp());
	}
	
	void Update()
	{
		if (showAdButton == true)
		{
			if (failedToLoad || badSignal)
			{
				SetVariables();
				StartCoroutine(NoConnection());
			}
		
			if (this.rewardedAd.IsLoaded())
			{
				SetVariables();
				this.rewardedAd.Show();
			}
			
			StartCoroutine(CheckBadSignal());
			
		}
	}
	
	private void Initialize()
	{
		gameManager = GameManager.instance;
		
        #if UNITY_ANDROID
            // adUnitId = "ca-app-pub-2964040886574646/5411926434"; // Mine
			adUnitId = "ca-app-pub-3940256099942544/5224354917"; // Test
        #elif UNITY_IPHONE
            // adUnitId = "ca-app-pub-2964040886574646/4767415837"; // Mine
			adUnitId = "ca-app-pub-3940256099942544/1712485313"; // Test
        #else
            adUnitId = "unexpected_platform";
        #endif
	}
	
	private void SetVariables()
	{
		failedToLoad = false;
		showAdButton = false;
		badSignal = false;
	}
	
    public void StartAd()
    {
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
	
	IEnumerator WakeUp()
	{
		yield return new WaitForSecondsRealtime(3f);
		Initialize();
		SetVariables();
		StartAd();
	}
	
	IEnumerator NoConnection()
    {
        yield return new WaitForSecondsRealtime(.5f);
        GameObject.Find("UI Controller").GetComponent<UIController>().ShowNoConnectionPanel();
    }
	
	IEnumerator CheckBadSignal()
	{
		yield return new WaitForSecondsRealtime(3f);
		badSignal = true;
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
		gameManager.SetLives(1);
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdFailedToShow event received with message: " + args.Message);
		GameObject.Find("UI Controller").GetComponent<UIController>().ShowNoConnectionPanel();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdClosed event received");
		GameObject.Find("UI Controller").GetComponent<UIController>().ShowWeAreBackPanel();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = "Lives";
        double amount = 3;
        MonoBehaviour.print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);
		gameManager.SetLives(2);
    }
	
}