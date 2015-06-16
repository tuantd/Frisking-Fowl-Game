using UnityEngine;
using System.Collections;

public class BackgroundContainer : MonoBehaviour {

	public Background[] background;
	
	private int _showingBackground;
	private int _backgroundNum;

	// Use this for initialization
	void Start () {

		_backgroundNum = background.Length;

		Reset ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Reset () {
	
		_showingBackground = 0;
		background [_showingBackground].ResetFirst ();

		for (int i = 1; i < _backgroundNum; i++) {
			
			background [i].ResetSecond ();
		}
	}

	public void MoveFirst () {
	
		background [_showingBackground].MoveFirst ();
	}

	public void MoveToScreenCenterCallback () {
		
		_showingBackground = (_showingBackground == 0) ? 1 : 0;
		
		background [_showingBackground].Move ();
	}
}
