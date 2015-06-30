using UnityEngine;
using System.Collections;

public class ShredderController : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag.Contains("Projectile")) {
			Destroy (other.gameObject);
		}
	}
}
