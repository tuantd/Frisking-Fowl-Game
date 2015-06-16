using UnityEngine;
using System.Collections;

public class CloudBase : MonoBehaviour {

	public static string ANI_MOVE = "CloudMove";

	public CloudContainer cloudContainer;
	public BoxCollider2D[] cloud;

	public ScoreListener[] cloudListener;

	protected Animation animation;

	// Use this for initialization
	void Start () {
	
		animation = gameObject.animation;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void MoveToScreenCenterCallback () {

		cloudContainer.MoveToScreenCenterCallback ();
	}

	public void Move () {

		animation.Stop (ANI_MOVE);

		transform.localPosition = new Vector3 (0, 1136, 0);

		animation.Play (ANI_MOVE);

		for (int i = 0; i < cloudListener.Length; i++) {
			
			cloudListener [i].Reset ();
		}
	}

	public void GameOver () {

		for (int i = 0; i < cloud.Length; i++) {

			cloud [i].enabled = false;
		}
	}

	public void Reset () {

		for (int i = 0; i < cloud.Length; i++) {
			
			cloud [i].enabled = true;
		}

		for (int i = 0; i < cloudListener.Length; i++) {
			
			cloudListener [i].Reset ();
		}
	}
}
