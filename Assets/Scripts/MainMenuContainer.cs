using UnityEngine;
using System.Collections;

#if UNITY_ANDROID
using GooglePlayGames;
#endif

public class MainMenuContainer : MonoBehaviour {

	private static string IPHONE_WEBVIEW_URL = "http://hn.24h.com.vn/";
	private static string ANDROID_WEBVIEW_URL = "http://hn.24h.com.vn/";

	public AudioSource audio;
	public AudioClip screenChangeSound;

	public Animation newGameAni;

	public GameContainer gameContainer;

	//public WebViewObject webViewObject;
	public GoogleMobileAdsController adsController;

	private string _url;
	private bool _isAuthenticated;

	public bool IsAuthenticated
	{
		get { return _isAuthenticated; }
		set { _isAuthenticated = value; }
	}

	// Use this for initialization
	void Start () {
		Debug.Log (System.DateTime.Now);
		gameObject.SetActive (true);

		if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Main Menu");

		newGameAni.Stop ();
		newGameAni.Play ();

		adsController.ShowBanner ();

		_isAuthenticated = false;

		#if UNITY_ANDROID

		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();

		#endif

		Social.localUser.Authenticate (success => {
			if (success) {
				Debug.Log ("Authentication successful");
				string userInfo = "Username: " + Social.localUser.userName + 
					"\nUser ID: " + Social.localUser.id + 
						"\nIsUnderage: " + Social.localUser.underage;
				Debug.Log (userInfo);

				_isAuthenticated = true;
			}
			else {
				Debug.Log ("Authentication failed");

				_isAuthenticated = false;
			}
		});

//		webViewObject.Init((msg)=>{
//			Debug.Log(string.Format("CallFromJS[{0}]", msg));
//			Debug.Log ("******** " + msg);
//		});
//
//		webViewObject.SetMargins(5, 5, 5, 800);
//		webViewObject.SetVisibility(true);
//
//		_url = IPHONE_WEBVIEW_URL;
//		if (Application.platform == RuntimePlatform.Android) {
//
//			_url = ANDROID_WEBVIEW_URL;
//		}
//
//		switch (Application.platform) {
//		
//		case RuntimePlatform.Android:
//		case RuntimePlatform.OSXEditor:
//		case RuntimePlatform.OSXPlayer:
//		case RuntimePlatform.IPhonePlayer:
//			webViewObject.LoadURL(_url);
//			webViewObject.EvaluateJS(
//				"window.addEventListener('load', function() {" +
//				"	window.Unity = {" +
//				"		call:function(msg) {" +
//				"			var iframe = document.createElement('IFRAME');" +
//				"			iframe.setAttribute('src', 'unity:' + msg);" +
//				"			document.documentElement.appendChild(iframe);" +
//				"			iframe.parentNode.removeChild(iframe);" +
//				"			iframe = null;" +
//				"		}" +
//				"	}" +
//				"}, false);");
//			webViewObject.EvaluateJS(
//				"window.addEventListener('load', function() {" +
//				"	window.addEventListener('click', function() {" +
//				"		Unity.call('clicked');" +
//				"	}, false);" +
//				"}, false);");
//			break;
//		
//		case RuntimePlatform.OSXWebPlayer:
//		case RuntimePlatform.WindowsWebPlayer:
//			webViewObject.LoadURL(_url);
//			webViewObject.EvaluateJS(
//				"parent.$(function() {" +
//				"	window.Unity = {" +
//				"		call:function(msg) {" +
//				"			parent.unityWebView.sendMessage('WebViewObject', msg)" +
//				"		}" +
//				"	};" +
//				"	parent.$(window).click(function() {" +
//				"		window.Unity.call('clicked');" +
//				"	});" +
//				"});");
//			break;
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTap (GameObject go) {
		Debug.Log ("MainMenuContainer - OnTap " + go.name);
		
		switch (go.name) {
			case "Play" :
				StartCoroutine (ShowGameContainer ());
				
				break;
			case "Rate" :

				if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Rate - Main Menu");
				
				#if UNITY_IPHONE

				// SystemInfo.operatingSystem returns something like iPhone OS 6.1
				float osVersion = -1f;
				string versionString = SystemInfo.operatingSystem.Replace("iPhone OS ", "");
				osVersion = float.Parse (versionString.Substring(0, 1));

				Debug.Log ("iOS Version " + osVersion);
			
				string urlString = "itms-apps://itunes.apple.com/app/id876206110";
				if (osVersion < 7) {
			
					urlString = "itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id=876206110";
				}
			
				Application.OpenURL(urlString);
			
				#elif UNITY_ANDROID
				
				string urlString = "market://details?id=" + "netgame.studio.funnyduck";
			
				Application.OpenURL(urlString);

				#endif
			
				break;
			case "Rank" :

				if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Leaderboard - Main Menu");
			
				if (_isAuthenticated) {

					Social.ShowLeaderboardUI();
				}
				
				break;
			
		}
	}

	private IEnumerator ShowGameContainer () 
	{
		if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Play - Main Menu");
		
		audio.PlayOneShot (screenChangeSound);	
		yield return new WaitForSeconds (0.2f);
		gameObject.SetActive (false);
		gameContainer.Show ();
		
		adsController.HideBanner ();
	}
}
