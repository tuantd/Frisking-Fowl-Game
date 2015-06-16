using UnityEngine;
using System.Collections;
using System.IO; 


public class ShareContainer : MonoBehaviour {

	private static int PHOTO_W = 640;
	private static int PHOTO_H = 680;

	public UIRoot root;
	public Camera camera;

	public AudioSource audio;
	public AudioClip screenChangeSound;

	public GameObject mainMenuContainer;
	public GameObject resultContainer;
	public GameObject newBest;	

	public Popup popup;
	public ShareImage shareImage;

	public UISprite achieve;
	public UILabel best;
	public UILabel score;

	private float _score;
	private Vector2 photoStartPoint;

	private bool _isTappedShare;

	// Use this for initialization
	void Start () {

		_isTappedShare = false;
	
		gameObject.SetActive (false);

		float ratio = (float)root.activeHeight / Screen.height;
		
		float width = Mathf.Ceil(Screen.width * ratio * camera.orthographicSize);
		float height = Mathf.Ceil(Screen.height * ratio * camera.orthographicSize);

		//CallFBInit ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show (float scoreGot, bool isNewBest) {

		_score = scoreGot;

		float bestSaved = PlayerPrefs.GetFloat ("Best", 0);
		best.text = bestSaved.ToString ();

		if (isNewBest) {
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
	}

	void OnTap (GameObject go) {
		Debug.Log ("ShareContainer - OnTap " + go.name);
		
		switch (go.name) {
			case "PostContainer" :
				//if (_isTappedShare) return;
				//_isTappedShare = true;

				if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Share - Share");
			
				//CheckLoginToPost ();
				StartCoroutine (TakeScreenshot());
				break;
			case "CancelContainer" :
				//if (_isTappedShare) return;

				StartCoroutine (ShowResultContainer ());
				break;
		}
	}

	private IEnumerator ShowResultContainer ()
	{
		if (GoogleAnalytics.instance) GoogleAnalytics.instance.LogScreen("Cancel - Share");
		
		audio.PlayOneShot (screenChangeSound);
		yield return new WaitForSeconds (0.2f);
		gameObject.SetActive (false);
		resultContainer.SetActive (true);
	}
	
	private IEnumerator TakeScreenshot()
	{
		yield return new WaitForEndOfFrame();

		float ratio = (float) Screen.height / root.activeHeight;
		
		float width_h =  Mathf.Floor(320 * ratio / camera.orthographicSize);
		float height_h = Mathf.Floor(240 * ratio / camera.orthographicSize);
		float width =  Mathf.Floor(PHOTO_W * ratio / camera.orthographicSize);
		float height = Mathf.Floor(PHOTO_H * ratio / camera.orthographicSize);
		
		photoStartPoint = new Vector2 ((int)Screen.width  / 2 - width_h, (int)Screen.height / 2 - height_h);

		var tex = new Texture2D((int)width, (int)height, TextureFormat.RGB24, false);
		// Read screen contents into the texture
		tex.ReadPixels(new Rect(photoStartPoint.x, photoStartPoint.y, width, height), 0 , 0);
		tex.Apply();

		//popup.ShowWaitingPopup ();

		byte[] screenshot = tex.EncodeToPNG();

		string fileName = "FunnyDuck-" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png";
		
		System.IO.File.WriteAllBytes(Application.persistentDataPath + "/" + fileName, screenshot);

		yield return new WaitForSeconds (0.1f);

		shareImage.Share (_score, fileName);
		
		//var wwwForm = new WWWForm();
		//wwwForm.AddBinaryData("image", screenshot, fileName);
		//wwwForm.AddField("message", "I scored " + _score + " points at Frisking Fowl, a game of netGame Studio. \n\n#friskingfowl");
		
		//FB.API("me/photos", Facebook.HttpMethod.POST, Callback, wwwForm);
	}

	void Callback(FBResult result)
	{
		//_isTappedShare = false;

		if (result.Error != null) {
			
			Debug.Log("PostCallback Failed");

			//popup.ShowResultPopup (false);
		}
		else {
			
			Debug.Log("PostCallback OK");

			//popup.ShowResultPopup (true);

			audio.PlayOneShot (screenChangeSound);
			gameObject.SetActive (false);
			resultContainer.SetActive (true);
		}
	}
	
	private void CheckLoginToPost () {
		
		if (FB.IsLoggedIn) {
			Debug.Log("Logged In " + FB.UserId);

			//FB.API("me?fields=name", Facebook.HttpMethod.GET, UserNameGetCallback);

			StartCoroutine (TakeScreenshot());

		}
		else {
			Debug.Log("Goto Log In");
			FB.Login("email, publish_actions", LoginCallback);
		}
	}

	private void LoginCallback(FBResult result) {

		if (result.Error != null) {

			Debug.Log("LoginCallback Failed");

			//_isTappedShare = false;
		}
		else if (!FB.IsLoggedIn) {

			Debug.Log("LoginCallback Cancelled");

			//_isTappedShare = false;
		}
		else {

			Debug.Log("LoginCallback Logged In " + FB.UserId);

			//FB.API("me?fields=name", Facebook.HttpMethod.GET, UserNameGetCallback);

			StartCoroutine (TakeScreenshot());
		}
	}

	void UserNameGetCallback(FBResult result) {

		string get_data;

		if (result.Error != null)
		{                                                                      
			get_data = result.Text;
		}
		else
		{
			get_data = result.Text;
		}
		
		Debug.Log("Username Logged In " + get_data);
	}
	
	private void CallFBInit() {

		FB.Init (OnInitComplete, OnHideUnity);
	}

	private void OnInitComplete() {

		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
	}

	private void OnHideUnity(bool isGameShown) {

		Debug.Log("Is game showing? " + isGameShown);
	}


}
