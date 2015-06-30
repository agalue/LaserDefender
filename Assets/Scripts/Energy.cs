using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour {

	public float energyValue = 50;
	public float speed = 5f;

	void Update () {
		transform.Rotate(new Vector3(0,0,45) * Time.deltaTime * 5f);
	}
	
	public void Throw ()
	{
		GetComponent<Rigidbody2D>().velocity = Vector3.down * speed;
	}
	
	public float GetHealth ()
	{
		return energyValue;
	}
	
	public void Consume ()
	{
		Destroy (gameObject);
	}
}
