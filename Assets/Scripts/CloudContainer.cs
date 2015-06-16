using UnityEngine;
using System.Collections;

public class CloudContainer : MonoBehaviour {

	private static float CLOUD_START_POS_Y = 1136;

	public CloudEasy[] cloudEasy;

	private int _showingCloud;
	private int _cloudNum;

	// Use this for initialization
	void Start () {

		_cloudNum = cloudEasy.Length;

		_showingCloud = -1;
		_showingCloud = NextShowedCloud ();
	}
	
	// Update is called once per frame
	void Update () {


	}

	private int NextShowedCloud () {

		int randed;
		do {
			randed = Random.Range (0, _cloudNum);
		} while (randed == _showingCloud);
		
		return randed;
	}

	public void MoveToScreenCenterCallback () {

		_showingCloud = NextShowedCloud ();

		cloudEasy [_showingCloud].Move ();
	}

	public void GameStart () {

		cloudEasy [_showingCloud].Move ();

		for (int i = 0; i < _cloudNum; i++) {
				
			cloudEasy [i].Reset ();
		}
	}

	public void GameOver () {

		for (int i = 0; i < _cloudNum; i++) {
			
			cloudEasy [i].GameOver ();
		}
	}

	public void Reset () {

		Vector3 vector3;
		for (int i = 0; i < _cloudNum; i++) {

			cloudEasy [i].animation.Stop ();

			vector3   = cloudEasy [i].transform.localPosition;
			vector3.y = CLOUD_START_POS_Y;
			cloudEasy [i].transform.localPosition = vector3;
		}
	}
}
