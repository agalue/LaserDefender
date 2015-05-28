using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour
{
	private Text scoreUI;
	private int scoreValue;

	void Start ()
	{
		scoreUI = GetComponent<Text> ();
		Reset ();
	}
	
	public void Score (int points)
	{
		scoreValue += points;
		UpdateUI ();
	}
	
	public void Reset ()
	{
		scoreValue = 0;
		UpdateUI ();
	}

	void UpdateUI ()
	{
		scoreUI.text = scoreValue.ToString ();
	}	
}
