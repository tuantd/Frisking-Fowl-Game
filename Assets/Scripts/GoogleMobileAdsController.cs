using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class GoogleMobileAdsController : MonoBehaviour {

    private BannerView bannerView;

    void Start()
    {
        #if UNITY_ANDROID
			string adUnitId = "a1535e6f10e32e3";
        #elif UNITY_IPHONE
			string adUnitId = "a1535e6fb0c32ff";
        #else
			string adUnitId = "a1535e6f74724ae";
        #endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        // Register for ad events.
        bannerView.AdLoaded += HandleAdLoaded;
        bannerView.AdFailedToLoad += HandleAdFailedToLoad;
        bannerView.AdOpened += HandleAdOpened;
        bannerView.AdClosing += HandleAdClosing;
        bannerView.AdClosed += HandleAdClosed;
        bannerView.AdLeftApplication += HandleAdLeftApplication;

		RequestBanner ();
    }

//    void OnGUI() {
//        // Puts some basic buttons onto the screen.
//        GUI.skin.button.fontSize = (int) (0.05f * Screen.height);
//
//        Rect buttonRect = new Rect(0.1f * Screen.width, 0.1f * Screen.height,
//                                   0.8f * Screen.width, 0.2f * Screen.height);
//        if (GUI.Button(buttonRect, "Request Banner")) {
//            RequestBanner();
//        }
//
//        Rect bannerRect = new Rect(0.1f * Screen.width, 0.4f * Screen.height,
//                                   0.8f * Screen.width, 0.2f * Screen.height);
//        if (GUI.Button(bannerRect, "Show Banner")) {
//            ShowBanner();
//        }
//
//        Rect interstitialRect = new Rect(0.1f * Screen.width, 0.7f * Screen.height,
//                                         0.8f * Screen.width, 0.2f * Screen.height);
//        if (GUI.Button(interstitialRect, "Hide Banner")) {
//            HideBanner();
//        }
//    }

   	public void RequestBanner() {
        // Request a banner ad, with optional custom ad targeting.
        AdRequest request = new AdRequest.Builder()
            //.AddKeyword("game")
            .Build();
        bannerView.LoadAd(request);
    }

	public void ShowBanner() {
        bannerView.Show();
    }

	public void HideBanner() {
        bannerView.Hide();
    }

    #region Banner callback handlers

    public void HandleAdLoaded()
    {
        print("HandleAdLoaded event received.");
    }

    public void HandleAdFailedToLoad(string message)
    {
        print("HandleFailedToReceiveAd event received with message: " + message);
    }

    public void HandleAdOpened()
    {
        print("HandleAdOpened event received");
    }

    void HandleAdClosing ()
    {
        print("HandleAdClosing event received");
    }

    public void HandleAdClosed()
    {
        print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication()
    {
        print("HandleAdLeftApplication event received");
    }

    #endregion
}
