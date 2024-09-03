using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour {
    private string AppID = "ca-app-pub-1998790013995705~7779058188";
    private string BannerID = "ca-app-pub-1998790013995705/1863003631";
    private string InterstitialID = "ca-app-pub-1998790013995705/4010654827";

	// Use this for initialization
	void Awake () {
        MobileAds.Initialize(AppID);
	}
    public void RequestBannerAd()
    {
        BannerView Ad = new BannerView(BannerID, AdSize.Banner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();
        Ad.LoadAd(request);
    }
    public void RequestInterstitialAd()
    {
        InterstitialAd Ad = new InterstitialAd(InterstitialID);
        AdRequest request = new AdRequest.Builder().Build();
        Ad.LoadAd(request);
    }
}
