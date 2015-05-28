using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
	private static MusicPlayer instance = null;

	void Awake ()
	{
		if (instance == null) {
			// To save the first instance as a static variable
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			// To avoid duplicates
			Destroy (gameObject);
		}
	}
	
}
