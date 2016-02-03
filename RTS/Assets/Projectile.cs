using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {


	public float speed;
	public float range;

	private float distance;


	// Update is called once per frame
	void Update () 
	{
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
		distance += Time.deltaTime * speed;
		if(distance >= range)
			Destroy(gameObject);
	}
}
