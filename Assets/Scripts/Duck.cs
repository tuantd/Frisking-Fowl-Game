using UnityEngine;
using System.Collections;

public class Duck : MonoBehaviour {

	public CloudContainer cloudContainer;
	public BackgroundContainer backgroundContainer;

	public GameObject duckDie;
	public GameObject duckIdle;
	public GameObject hand;

	public AudioSource audio;
	public AudioSource audioSmall;
	public AudioSource audioVerySmall;
	public AudioClip flySound;
	public AudioClip collisionSound;
	public AudioClip fallSound;
	public AudioClip screenChangeSound;

	public UISpriteAnimation duckIdleSA;

	private Rigidbody2D _rigidBody2D;
	private Vector2 _moveDirection;
	private bool _isTouched;
	private bool _isFalling;

	// Use this for initialization
	void Start () {
		Debug.Log ("Duck - Start");

		_rigidBody2D = gameObject.GetComponent<Rigidbody2D> ();

		Reset ();
	}

	// Update is called once per frame
	void Update () {

		_isFalling = (_rigidBody2D.velocity.y <= 0);
		if (_isFalling) {
			//Idle ();
		}
	}

	void OnTap (GameObject go) {
		
		if (_isTouched == false) {

			_isTouched = true;

			hand.SetActive (false);

			_rigidBody2D.gravityScale = Constants.GRAVITY_SCALE;//PlayerPrefs.GetFloat (Constants.GRAVITY_SCALE_PP, Constants.GRAVITY_SCALE);
			_rigidBody2D.WakeUp ();

			duckIdleSA.framesPerSecond = 15;

			cloudContainer.GameStart ();
			backgroundContainer.MoveFirst ();
		}

		float forceScaleX = Constants.FORCE_SCALE_X; //PlayerPrefs.GetFloat (Constants.FORCE_SCALE_X_PP, Constants.FORCE_SCALE_X);

		switch (go.name) {
			case "RightButton" :
				audioVerySmall.PlayOneShot (flySound);
				_moveDirection = new Vector2 (forceScaleX, 1);
				Left ();
				break;
			case "LeftButton" :
				audioVerySmall.PlayOneShot (flySound);
				_moveDirection = new Vector2 (- forceScaleX, 1);
				Right ();
				break;
		}

		if (_isFalling) {
			
			//_rigidBody2D.AddForce (_moveDirection * PlayerPrefs.GetFloat (Constants.FORCE_PP, Constants.FORCE));
		}

		_rigidBody2D.velocity = Vector2.zero;
		_rigidBody2D.AddForce (_moveDirection * Constants.FORCE); //PlayerPrefs.GetFloat (Constants.FORCE_PP, Constants.FORCE));
	}

	public IEnumerator Die (bool isDiedByGround)
	{
		audio.PlayOneShot (collisionSound);
		duckDie.SetActive (true);
		duckIdle.SetActive (false);
		if (!isDiedByGround) {
			yield return new WaitForSeconds (0.2f);
			audioSmall.PlayOneShot (fallSound);
		}
	}

	public void Idle ()
	{
		duckIdleSA.framesPerSecond = 8;
		duckIdle.SetActive (true);
		duckDie.SetActive (false);
	}

	public void Left ()
	{
	}

	public void Right ()
	{
	}

	public void Reset () {

		_isTouched    = false;
		_isFalling    = false;

		_rigidBody2D.gravityScale = 0f;
		_rigidBody2D.Sleep ();

		transform.localPosition = new Vector3 (0, -350, -1);
		transform.localRotation = Quaternion.identity;

		hand.SetActive (true);
		
		// play idle animation
		Idle ();
	}
}
