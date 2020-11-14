using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class AdManager : MonoBehaviour
{
    private RewardedAd rewardedAd;
	private UIController uiController;
	private GameManager gameManager;
	
    public void StartAd()
    {
		gameManager = GameManager.instance;
		uiController = GameObject.Find("UI Controller").GetComponent<UIController>();
		
		string adUnitId;
        //adUnitId = "ca-app-pub-2964040886574646/5411926434";
		adUnitId = "ca-app-pub-3940256099942544/5224354917";

        this.rewardedAd = new RewardedAd(adUnitId);
		
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
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
		if (this.rewardedAd.IsLoaded())
		{
			this.rewardedAd.Show();
		}
		
		else
		{
			uiController.ShowAdTryAgain();
		}
	}
	
	public void ShowAdTryAgain()
	{
		if (this.rewardedAd.IsLoaded())
		{
			this.rewardedAd.Show();
		}
		
		else
		{
			uiController.ShowNoConnectionPanel();
		}
	}
	
	public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdClosed event received");
		gameManager.SetLives(3);
		uiController.ShowWeAreBackPanel();
    }
	
}