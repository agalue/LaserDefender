using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float health = 1000;
	public float speed = 15.0f;
	public float padding = 0.5f;
	public float fireRate = 0.5f;
	
	public AudioClip fireSound;
	public float fireSoundVolume = 1f;
	public AudioClip deathSound;
	public float deathSoundVolume = 1f;
	public GameObject projectilePrefab;
	public GameObject explosionPrefab;
	
	private float xMin, xMax;
	private float nextFire = 0.0f;
	private float healthRemaining;
	private LevelManager levelManager;
	private Slider healthSlider;
	
	void Awake ()
	{
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		healthSlider = GameObject.FindObjectOfType<Slider> (); // There is only one slider object.
		healthRemaining = health;
		healthSlider.maxValue = health;
		healthSlider.value = health;
	}

	void Start ()
	{
		// Setting limits on X Axis based on the Camera's Viewport.
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToCamera));
		xMin = leftBoundary.x + padding;
		xMax = rightBoundary.x - padding;
	}

	void Update ()
	{
		// Move the player
		float moveHorizontal = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
		transform.Translate (new Vector3 (moveHorizontal, 0, 0));

		// Restrict the player's movement to the Viewport width.
		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp (pos.x, xMin, xMax);
		transform.position = pos;
		
		// Fire
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Fire ();
		}
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("EnemyProjectile")) {
			Projectile missile = other.gameObject.GetComponent<Projectile> ();
			healthRemaining -= missile.GetDamage ();
			healthSlider.value = healthRemaining;
			missile.Hit ();
			if (healthRemaining <= 0) {
				Die ();
			}
		}
		if (other.gameObject.CompareTag ("Energy")) {
			if (healthRemaining < health) { // If the player has maximum health, picking energy doesn't make sense.
				Energy energy = other.gameObject.GetComponent<Energy> ();
				healthRemaining += energy.GetHealth ();
				healthSlider.value = healthRemaining;
				energy.Consume ();
			}
		}
	}
		
	void Fire ()
	{
		GameObject projectile = Instantiate (projectilePrefab, transform.position, Quaternion.identity) as GameObject;
		projectile.GetComponent<Projectile> ().Launch (Vector3.up);
		AudioSource.PlayClipAtPoint (fireSound, transform.position, fireSoundVolume);
	}
	
	void Die ()
	{
		AudioSource.PlayClipAtPoint (deathSound, transform.position, deathSoundVolume);
		Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		Destroy (gameObject);
		levelManager.GameOver ();
	}
}
