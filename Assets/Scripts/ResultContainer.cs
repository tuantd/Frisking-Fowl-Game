using UnityEngine;
using System.Collections;

public class ResultContainer : MonoBehaviour {

	public GameObject mainMenuContainer;
	public GameObject newBest;
	public GameObject light;
	public GameObject hand;

	public GoogleMobileAdsController adsController;
	public GameContainer gameContainer;
	public ShareContainer shareContainer;

	public Animation cameraShotAni;

	public UISprite achieve;
	public UILabel best;
	public UILabel score;

	public AudioSource audio;
	public AudioClip cameraSound;
	public AudioClip screenChangeSound;

	private float _score;
	private bool _isNewBest;

	// Use this for initialization
	void Start () {
	
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show (float scoreGot) {

		if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Result");

		light.SetActive (false);
		hand.SetActive (false);

		_score = scoreGot;

		float bestSaved = PlayerPrefs.GetFloat ("Best", 0);
		if (bestSaved < scoreGot) {
			best.text = scoreGot.ToString ();
			PlayerPrefs.SetFloat ("Best", scoreGot);
			_isNewBest = true;
		}
		else {
			best.text = bestSaved.ToString ();
			_isNewBest = false;
		}

		if (_isNewBest) {
			newBest.SetActive (true);
		}
		else {
			newBest.SetActive (false);
		}

		score.text = scoreGot.ToString ();

		if (scoreGot <= 5) {
			achieve.spriteName = "die_big_1";
		}
		else if (scoreGot <= 10) {
			achieve.spriteName = "die_big_2";
		}
		else if (scoreGot <= 20) {
			achieve.spriteName = "die_big_3";
		}
		else if (scoreGot <= 50) {
			achieve.spriteName = "die_big_4";
		}
		else {
			achieve.spriteName = "die_big_5";
		}

		gameObject.SetActive (true);

		if (mainMenuContainer.GetComponent<MainMenuContainer>().IsAuthenticated) {
			
#if UNITY_IPHONE

			Social.ReportScore((long)scoreGot, "friskingfowl.leaderboard1", (bool success) => {
				// handle success or failure
			});

#elif UNITY_ANDROID

			Social.ReportScore((long)scoreGot, "CgkIzYKW44AeEAIQBg", (bool success) => {
				// handle success or failure
			});
#endif
		}
		
		cameraShotAni.Stop ();
		cameraShotAni.Play ();

		StartCoroutine (ShowHand ());
	}

	public IEnumerator ShowHand () {

		yield return new WaitForSeconds (2.0f);
		hand.SetActive (true);
	}

	void OnTap (GameObject go) {
		Debug.Log ("ResultContainer - OnTap " + go.name);
		
		switch (go.name) {
		case "Retry" :
			StartCoroutine (ShowGameContainer ());

			break;
		case "Rank" :

			if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Leaderboard - Result");

			if (mainMenuContainer.GetComponent<MainMenuContainer>().IsAuthenticated) {
				
				Social.ShowLeaderboardUI();
			}

			break;
		case "CameraShot" :

			if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Share - Result");

			StartCoroutine (TakePhoto ());
			break;
		}
	}

	private IEnumerator ShowGameContainer () {

		if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Retry - Result");
		
		audio.PlayOneShot (screenChangeSound);
		yield return new WaitForSeconds (0.2f);
		gameObject.SetActive (false);
		gameContainer.Show ();
		
		adsController.HideBanner ();
	}

	private IEnumerator TakePhoto () {

		light.SetActive (true);
		audio.PlayOneShot (cameraSound);
		yield return new WaitForSeconds (0.5f);
		light.SetActive (false);

		gameObject.SetActive (false);
		shareContainer.Show (_score, _isNewBest);
	}
}
