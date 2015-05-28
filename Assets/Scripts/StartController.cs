using UnityEngine;
using System.Collections;

public class StartController : MonoBehaviour
{
	private LevelManager levelManager;
	
	void Start ()
	{
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			levelManager.InitializeGame ();
		}
	
	}
}
