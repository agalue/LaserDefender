using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
		SceneManager.LoadScene ("Game");
	}

	public void GameOver ()
	{
		StartCoroutine (Finish ());
	}

	public void Restart ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	private IEnumerator Finish ()
	{
		yield return new WaitForSeconds (1f);
		gameOverUI.SetActive (true);
		gameOverUI.GetComponent<Animator> ().Play ("GameOverInit");
	}

}
