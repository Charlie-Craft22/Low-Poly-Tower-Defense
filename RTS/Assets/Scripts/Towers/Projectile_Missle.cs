using UnityEngine;
using System.Collections;

public class Projectile_Missle : MonoBehaviour 
{
	public Transform explosion_Prefab;
	public Transform target;
	public float range = 10;
	public float speed = 10;

	private float distance;

	void Update()
	{
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
		distance += Time.deltaTime * speed;
		if(distance >= range)
			Explode();
		if(target)
			transform.LookAt(target);
		else
			Explode();
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Explosion");
		if(other.gameObject.tag == "Enemy")
		{
			Explode();
		}
	}

	void Explode()
	{
		Instantiate(explosion_Prefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

}
