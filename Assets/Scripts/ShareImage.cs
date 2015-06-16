using UnityEngine;
using System.Collections;

public class ShareImage : MonoBehaviour {

	public Diffusion diffusion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#if UNITY_ANDROID

	public void Share (float score, string filename) {

		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<AndroidJavaObject>("ACTION_SEND"));
		
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","file://" + Application.persistentDataPath + "/" + filename);
	
		string text = "I just scored " + score + " points in Frisking Fowl, one of the hardest game in the World. Can you beat me?\n#friskingfowl available on iOS and Android: http://bit.ly/friskingfowl";
		
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<AndroidJavaObject>("EXTRA_STREAM"), uriObject);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Frisking Fowl");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), text);
		
		intentObject.Call<AndroidJavaObject>("setType", "image/png");
		
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		
		currentActivity.Call("startActivity", intentObject);
	}
	
	#elif UNITY_IPHONE
	
	public void Share (float score, string filename) {

		string text = "I just scored " + score + " points in Frisking Fowl, one of the hardest game in the World. Can you beat me?\n#friskingfowl available on iOS and Android: http://bit.ly/friskingfowl";
	
		diffusion.Share(text, null, "file://" + Application.persistentDataPath + "/" + filename);
	}

	#endif
}
