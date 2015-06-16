using UnityEngine;
using System.Collections;

public class GameContainer : MonoBehaviour {

	public AudioSource audio;
	public AudioClip scoreGotSound;
	
	public GameObject controllerButton;

	public UILabel scoreLabel;

	public GoogleMobileAdsController adsController;
	public MainMenuContainer mainMenuContainer;
	public Duck duck;
	public CloudContainer cloudContainer;
	public BackgroundContainer backgroundContainer;
	public ResultContainer resultContainer;
	public Animation cameraAnimation;
	public float resetGameTime;
	
	private bool  _gameOver;
	private bool  _isDiedByGround;
	private float _timeCount;

	private float _score;
	
	public bool GameOver
	{
		get { return _gameOver; }
		set { _gameOver = value; }
	}

	public bool IsDiedByGround
	{
		get { return _isDiedByGround; }
		set { _isDiedByGround = value; }
	}

	// Use this for initialization
	void Start () {

		_gameOver  = false;
		_timeCount = 0;
	
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (_gameOver == false) return;
		
		// Game over process
		if (_timeCount == 0) {
			
			Debug.Log ("GameContainer - Update - GameOver ");
			
			controllerButton.SetActive (false);
			DuckDie ();
			cloudContainer.GameOver ();
			cameraAnimation.Stop ();
			cameraAnimation.Play ();
			//Handheld.Vibrate ();
		}
		
		// Reset game process
		_timeCount += Time.deltaTime;
		if (_timeCount >= resetGameTime) {
			
			Debug.Log ("GameContainer - Update - Reset ");
			
			duck.Reset ();
			cloudContainer.Reset ();
			backgroundContainer.Reset ();
			
			_gameOver  = false;
			_timeCount = 0;

			resultContainer.Show (_score);
			controllerButton.SetActive (true);
			gameObject.SetActive (false);
		}
	}

	private void DuckDie ()
	{
		adsController.ShowBanner ();

		StartCoroutine (duck.Die (_isDiedByGround));
	}

	public void Show () {

		ResetScore ();
		gameObject.SetActive (true);
		adsController.RequestBanner ();
	}

	private void ResetScore () {

		_score = 0;
		scoreLabel.text = "0";
	}

	public void UpdateScore () {

		audio.PlayOneShot (scoreGotSound);

		_score ++;
		scoreLabel.text = _score.ToString ();

		if (mainMenuContainer.IsAuthenticated == false) return;

		#if UNITY_IPHONE

		if (_score == 5) {
			
			Social.ReportProgress("5points", 100.0f, (bool success) => {
				// handle success or failure
			});
			
			if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Achieve 5 - Game");
		}
		else if (_score == 10) {
			
			Social.ReportProgress("10points", 100.0f, (bool success) => {
				// handle success or failure
			});
			
			if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Achieve 10 - Game");
		}
		else if (_score == 20) {
			
			Social.ReportProgress("20points", 100.0f, (bool success) => {
				// handle success or failure
			});
			
			if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Achieve 20 - Game");
		}
		else if (_score == 30) {
			
			Social.ReportProgress("30points", 100.0f, (bool success) => {
				// handle success or failure
			});
			
			if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Achieve 30 - Game");
		}
		else if (_score == 50) {
			
			Social.ReportProgress("50points", 100.0f, (bool success) => {
				// handle success or failure
			});
			
			if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Achieve 50 - Game");
		}
		else if (_score == 100) {
			
			Social.ReportProgress("100points", 100.0f, (bool success) => {
				// handle success or failure
			});
			
			if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Achieve 100 - Game");
		}
		
		#elif UNITY_ANDROID

		if (_score == 100) {
			
			Social.ReportProgress("CgkIzYKW44AeEAIQBQ", 100.0f, (bool success) => {
				// handle success or failure
			});

			if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Achieve 100 - Game");
		}
		else if (_score == 50) {
			
			Social.ReportProgress("CgkIzYKW44AeEAIQBA", 100.0f, (bool success) => {
				// handle success or failure
			});

			if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Achieve 50 - Game");
		}
		else if (_score == 30) {
			
			Social.ReportProgress("CgkIzYKW44AeEAIQAw", 100.0f, (bool success) => {
				// handle success or failure
			});
		}
		else if (_score == 20) {
			
			Social.ReportProgress("CgkIzYKW44AeEAIQAg", 100.0f, (bool success) => {
				// handle success or failure
			});

			if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Achieve 20 - Game");
		}
		else if (_score == 10) {
			
			Social.ReportProgress("CgkIzYKW44AeEAIQAQ", 100.0f, (bool success) => {
				// handle success or failure
			});

			if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Achieve 10 - Game");
		}
		else if (_score == 5) {
			
			Social.ReportProgress("CgkIzYKW44AeEAIQAA", 100.0f, (bool success) => {
				// handle success or failure
			});

			if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Achieve 5 - Game");
		}

		#endif
	}
}
