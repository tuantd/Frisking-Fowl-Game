using UnityEngine;
using System.Collections;

public class Setting : MonoBehaviour {

	private string gravityScale;
	private string force;
	private string forceScaleX;
	private string cloudSpeed;

	// Use this for initialization
	void Start () {
//
//		gravityScale = PlayerPrefs.GetFloat (Constants.GRAVITY_SCALE_PP, Constants.GRAVITY_SCALE).ToString();
//		force        = PlayerPrefs.GetFloat (Constants.FORCE_PP        , Constants.FORCE        ).ToString();
//		forceScaleX  = PlayerPrefs.GetFloat (Constants.FORCE_SCALE_X_PP, Constants.FORCE_SCALE_X).ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	void OnGUI () {
//
//		GUI.Label (new Rect (25, 25, 200, 40), "Gravity Scale");
//		gravityScale = GUI.TextField (new Rect (225, 25, 100, 40), gravityScale);
//
//		GUI.Label (new Rect (25, 75, 200, 40), "Force");
//		force        = GUI.TextField (new Rect (225, 75, 100, 40), force);
//
//		GUI.Label (new Rect (25, 125, 200, 40), "Force Scale X");
//		forceScaleX  = GUI.TextField (new Rect (225, 125, 100, 40), forceScaleX);
//
//		if (GUI.Button (new Rect (25, 175, 100, 50), "Save")) {
//
//			Debug.Log ("Saved");
//
//			PlayerPrefs.SetFloat (Constants.GRAVITY_SCALE_PP, float.Parse(gravityScale));
//			PlayerPrefs.SetFloat (Constants.FORCE_PP        , float.Parse(force));
//			PlayerPrefs.SetFloat (Constants.FORCE_SCALE_X_PP, float.Parse(forceScaleX));
//		}
//	}
}
