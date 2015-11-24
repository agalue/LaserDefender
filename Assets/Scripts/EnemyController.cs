using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	public int scorePoints = 150;
	public float health = 300;
	public float shotsPerSecond = 0.5f;
	public float energyThrowProbability = 0.5f;
	
	public AudioClip fireSound;
	public float fireSoundVolume = 1f;
	public GameObject projectilePrefab;
	public GameObject explosionPrefab;
	public GameObject energyPrefab;
	
	private ScoreKeeper scoreKeeper;

	void Start ()
	{
		scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper> ();
	}
	
	void Update ()
	{
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability) {
			Fire ();
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("PlayerProjectile")) { // Not necessary as we have colision layers
			Projectile missile = other.gameObject.GetComponent<Projectile> ();
			health -= missile.GetDamage ();
			missile.Hit ();
			if (health <= 0) {
				Die ();
			}
		}
	}
	
	void Fire ()
	{
		GameObject projectile = Instantiate (projectilePrefab, transform.position, Quaternion.identity) as GameObject;
		projectile.GetComponent<Projectile> ().Launch (Vector3.down);
		AudioSource.PlayClipAtPoint (fireSound, transform.position, fireSoundVolume);
	}
	
	void Die ()
	{
		Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		Destroy (gameObject);
		scoreKeeper.Score (scorePoints);
		if (Random.value > 1 - energyThrowProbability) {
			ThrowEnergy ();
		}
	}
	
	void ThrowEnergy ()
	{
		GameObject energy = Instantiate (energyPrefab, transform.position, Quaternion.identity) as GameObject;
		energy.GetComponent<Energy> ().Throw ();
	}
}
