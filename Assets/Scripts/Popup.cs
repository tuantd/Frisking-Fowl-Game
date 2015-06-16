using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour {

	public GameObject duckContainer;
	public GameObject resultLabelGO;
	public GameObject waitLabel;
	public GameObject button;

	public UILabel resultLabel;

	// Use this for initialization
	void Start () {
	
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowWaitingPopup () {

		gameObject.SetActive (true);

		resultLabelGO.SetActive (false);
		button.SetActive (false);
		waitLabel.SetActive (true);
		duckContainer.SetActive (true);
	}

	public void ShowResultPopup (bool isSuccessful) {
		
		gameObject.SetActive (true);

		waitLabel.SetActive (false);
		duckContainer.SetActive (false);
		resultLabelGO.SetActive (true);
		button.SetActive (true);

		resultLabel.text = isSuccessful ? "Share Succesfully!" : "Share Failed!";
	}

	void OnTap (GameObject go) {
		Debug.Log ("Popup - OnTap " + go.name);
		
		switch (go.name) {
		case "OkContainer" :
			gameObject.SetActive (false);
			break;
		}
	}
}
