using UnityEngine;
using System.Collections;

public class WallContainer : MonoBehaviour {

	public UIRoot root;
	public Camera camera;
	
	public Transform bottomWall;
	public Transform leftWall;
	public Transform rightWall;

	// Use this for initialization
	void Start () {

		float ratio = (float)root.activeHeight / Screen.height;
		
		float width = Mathf.Ceil(Screen.width * ratio * camera.orthographicSize);
		float height = Mathf.Ceil(Screen.height * ratio * camera.orthographicSize);

		Vector3 vector3;

		vector3   = leftWall.localPosition;
		vector3.x = - width  / 2;
		leftWall.localPosition   = vector3;

		vector3   = rightWall.localPosition;
		vector3.x =   width  / 2;
		rightWall.localPosition  = vector3;

		vector3   = bottomWall.localPosition;
		vector3.y = - height * 2   ;
		bottomWall.localPosition = vector3;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
