using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public enum AdsUnit
{ 
    전면,배너
}


public class GoogleAdMob : MonoBehaviour
{

    public AdsUnit adsUnit = AdsUnit.배너;

    public static int mainSceneCount = 0;

    private InterstitialAd interstitialAd;


    private void Awake()
    {
        RequestInterstitial();
    }

    private string GetUnitID(string str)
    {
        if (str == "test")
            return "ca-app-pub-3940256099942544/1033173712"; //테스트아이디
        else
            return "ca-app-pub-5205187543072249/8717266450"; //전면광고아이디
    }   

    private void RequestInterstitial()
    {

        string adUnitID = GetUnitID("test");

        this.interstitialAd = new InterstitialAd(adUnitID);


        interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        interstitialAd.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        interstitialAd.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        interstitialAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitialAd.LoadAd(request);
    }

    public  void AdsPlay()
    {
        mainSceneCount++;

        if (mainSceneCount >= 3)
        {
            mainSceneCount = 0;

            if (interstitialAd.IsLoaded()) this.interstitialAd.Show();
            else RequestInterstitial();
        }
      
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        print("HandleAdClosed event received");

        interstitialAd.Destroy();

        RequestInterstitial();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        print("HandleAdLeavingApplication event received");
    }

}
