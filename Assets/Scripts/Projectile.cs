using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	public float speed = 10f;
	public float damage = 100f;
	
	public void Hit ()
	{
		Destroy (gameObject);
	}
	
	public float GetDamage ()
	{
		return damage;
	}
	
	public void Launch (Vector3 direction)
	{
		rigidbody2D.velocity = direction * speed;
	}
}
