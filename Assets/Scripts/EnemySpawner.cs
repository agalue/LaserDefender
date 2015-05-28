using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemyPrefab;
	public float padding = 0.5f;
	public float speed = 5f;
	public float spawnDelay = 0.5f;
	
	private float xMin, xMax;
	private bool movingRight = true;
		
	void Start ()
	{
		// Setting limits on X Axis based on the Camera's Viewport.
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToCamera));
		xMin = leftBoundary.x + padding;
		xMax = rightBoundary.x - padding;

		SpawnUntilFull ();
	}
	
	void Update ()
	{
		// Moving formation from left to right and viceversa
		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
			// Switch direction when reaching the right border
			if (GetRightEdgeOfFormation () > xMax) {
				movingRight = false;
			}
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
			// Switch direction when reaching the left border
			if (GetLeftEdgeOfFormation () < xMin) {
				movingRight = true;
			}
		}
		// Respawn formation
		if (AllMembersDead ()) {
			SpawnUntilFull ();
		}
	}
	
	float GetRightEdgeOfFormation ()
	{
		float edge = xMin;
		foreach (Transform t in transform) {
			if (t.childCount > 0 && t.position.x >= edge) {
				edge = t.position.x;
			}
		}
		return edge;
	}

	float GetLeftEdgeOfFormation ()
	{
		float edge = xMax;
		foreach (Transform t in transform) {
			if (t.childCount > 0 && t.position.x <= edge) {
				edge = t.position.x;
			}
		}
		return edge;
	}
	
	void SpawnUntilFull ()
	{
		StartCoroutine (SpawnEnemies ());
	}

	// Assumes there are no enemies on the screen
	IEnumerator SpawnEnemies ()
	{
		for (int i=0; i < transform.childCount; i++) {
			Transform location = NextFreePosition ();
			GameObject enemy = Instantiate (enemyPrefab, location.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = location;
			yield return new WaitForSeconds (spawnDelay);
		}
	}

	// Verify if each position object has an enemy
	bool AllMembersDead ()
	{
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}
	
	Transform NextFreePosition ()
	{
		List<Transform> availablePositions = new List<Transform> ();
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				availablePositions.Add (childPositionGameObject);
			}
		}
		if (availablePositions.Count > 0) {
			int index = Random.Range (0, availablePositions.Count - 1);
			return availablePositions [index];
		}
		return null;
	}
	
}
