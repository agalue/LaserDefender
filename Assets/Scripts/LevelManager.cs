using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour
{
	private GameObject gameOverUI;

	void Awake ()
	{
		gameOverUI = GameObject.Find ("GameOver");
		if (gameOverUI) {
			gameOverUI.SetActive (false);
		}
	}

	public void InitializeGame ()
	{
		Application.LoadLevel ("Game");
	}

	public void GameOver ()
	{
		StartCoroutine (Finish ());
	}

	public void Restart ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	private IEnumerator Finish ()
	{
		yield return new WaitForSeconds (1f);
		gameOverUI.SetActive (true);
		gameOverUI.GetComponent<Animator> ().Play ("GameOverInit");
	}

}
