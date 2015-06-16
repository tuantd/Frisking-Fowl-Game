using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject mainMenuContainer;
	public GameObject gameContainer;
	public GameObject resultContainer;
	public GameObject shareContainer;
	public GameObject popup;
	public GameObject adsController;

	// Use this for initialization
	void Start () {
	
		adsController.SetActive (true);
		mainMenuContainer.SetActive (true);
		gameContainer.SetActive (true);
		resultContainer.SetActive (true);
		shareContainer.SetActive (true);
		popup.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
