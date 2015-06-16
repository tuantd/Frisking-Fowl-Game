using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	public GameContainer gameContainer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D coll) {

		gameContainer.IsDiedByGround = false;
		gameContainer.GameOver = true;
	}
}
