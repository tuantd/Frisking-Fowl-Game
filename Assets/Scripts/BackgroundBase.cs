using UnityEngine;
using System.Collections;

public class BackgroundBase : MonoBehaviour {

	public static string ANI_BG_MOVE_1 = "BackgroundMove1";
	public static string ANI_BG_MOVE_2 = "BackgroundMove2";

	public UIRoot root;
	public Camera camera;

	public GameObject hourse;
	public GameObject cloudBG1;

	public UISprite bottomWall1;
	public UISprite bottomWall2;
	public UISprite leftWall;
	public UISprite rightWall;

	public UISprite[] leaf;

	public BackgroundContainer backgroundContainer;

	protected Animation animation;

	private string[] leafName;

	// Use this for initialization
	void Start () {
	
		animation = gameObject.animation;

		leafName = new string[2];
		leafName[0] = "random1";
		leafName[1] = "random2";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveToScreenCenterCallback () {
		
		backgroundContainer.MoveToScreenCenterCallback ();
	}

	public void ResetFirst () {

		animation.Stop (ANI_BG_MOVE_1);
		animation.Stop (ANI_BG_MOVE_2);
		
		bottomWall1.active = true;
		bottomWall2.active = true;
		leftWall.spriteName = "tree_l";
		rightWall.spriteName = "tree_r";
		leftWall.transform.localScale = new Vector3 (39, 1136, 1);
		rightWall.transform.localScale = new Vector3 (39, 1136, 1);
		transform.localPosition = new Vector3 (0, 0, 0);

		float ratio = (float)root.activeHeight / Screen.height;
		
		float width = Mathf.Ceil(Screen.width * ratio * camera.orthographicSize);

		
		Vector3 vector3;
		
		vector3   = leftWall.transform.localPosition;
		vector3.x = - width  / 2 - 1;
		leftWall.transform.localPosition   = vector3;
		
		vector3   = rightWall.transform.localPosition;
		vector3.x =   width  / 2 + 1;
		rightWall.transform.localPosition  = vector3;

		cloudBG1.SetActive (false);
		hourse.SetActive (true);

		vector3   = hourse.transform.localScale;
		vector3.x = width;
		hourse.transform.localScale = vector3;

		int randed = 0;
		for (int i = 0; i < leaf.Length; i++) {
			
			randed = Random.Range (0, 3);
			leaf[i].gameObject.SetActive (randed > 1 ? true: false);

			if (randed == 0) continue;

			randed = Random.Range (0, 2);
			leaf[i].spriteName = leafName[randed];

			vector3 = leaf[i].transform.localPosition;
			if (i < 3) {
				vector3.x = - width  / 2 + 21;
			}
			else {
				vector3.x =   width  / 2 - 3;
			}
			leaf[i].transform.localPosition = vector3;
		}
	}

	public void ResetSecond () {

		animation.Stop (ANI_BG_MOVE_1);
		animation.Stop (ANI_BG_MOVE_2);
		
		bottomWall1.active = false;
		bottomWall2.active = false;
		leftWall.spriteName = "tree_2x2";
		rightWall.spriteName = "tree_2x2";
		leftWall.transform.localScale = new Vector3 (6, 1136, 1);
		rightWall.transform.localScale = new Vector3 (6, 1136, 1);
		transform.localPosition = new Vector3 (0, 1136, 0);
		
		float ratio = (float)root.activeHeight / Screen.height;
		
		float width = Mathf.Ceil(Screen.width * ratio * camera.orthographicSize);
		
		Vector3 vector3;
		
		vector3   = leftWall.transform.localPosition;
		vector3.x = - width  / 2 - 1;
		leftWall.transform.localPosition   = vector3;
		
		vector3   = rightWall.transform.localPosition;
		vector3.x =   width  / 2 + 1;
		rightWall.transform.localPosition  = vector3;

		int randed = 0;
		for (int i = 0; i < leaf.Length; i++) {
			
			randed = Random.Range (0, 3);
			leaf[i].gameObject.SetActive (randed > 1 ? true: false);
			
			if (randed == 0) continue;
			
			randed = Random.Range (0, 2);
			leaf[i].spriteName = leafName[randed];
			
			vector3 = leaf[i].transform.localPosition;
			if (i < 3) {
				vector3.x = - width  / 2 + 21;
			}
			else {
				vector3.x =   width  / 2 - 3;
			}
			leaf[i].transform.localPosition = vector3;
		}

		cloudBG1.SetActive (true);
		hourse.SetActive (false);
	}
	
	public void MoveFirst () {

		bottomWall1.active = true;
		bottomWall2.active = true;
		leftWall.spriteName = "tree_l";
		rightWall.spriteName = "tree_r";
		leftWall.transform.localScale = new Vector3 (39, 1136, 1);
		rightWall.transform.localScale = new Vector3 (39, 1136, 1);

		animation.Stop (ANI_BG_MOVE_1);
		animation.Play (ANI_BG_MOVE_1);
	}

	public void Move () {

		bottomWall1.active = false;
		bottomWall2.active = false;
		leftWall.spriteName = "tree_2x2";
		rightWall.spriteName = "tree_2x2";
		leftWall.transform.localScale = new Vector3 (6, 2000, 1);
		rightWall.transform.localScale = new Vector3 (6, 2000, 1);

		cloudBG1.SetActive (true);
		hourse.SetActive (false);

		animation.Stop (ANI_BG_MOVE_2);
		animation.Play (ANI_BG_MOVE_2);
	}
}
