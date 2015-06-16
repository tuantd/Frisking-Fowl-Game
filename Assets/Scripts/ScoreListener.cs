using UnityEngine;
using System.Collections;

public class ScoreListener : MonoBehaviour {

	public GameContainer gameContainer;

	public Transform cloud;
	public Transform duck;

	private bool isUpdatedScore;

	// Use this for initialization
	void Start () {
	
		Reset ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isUpdatedScore) return;

		if (transform.localPosition.y + cloud.localPosition.y <= duck.localPosition.y) {

			isUpdatedScore = true;
			
			gameContainer.UpdateScore ();
		}
	}

	public void Reset () {

		isUpdatedScore = false;
	}
}
