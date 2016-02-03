using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour 
{

	public GameObject projectile_Prefab;
	public float reloadTime;
	public float turnSpeed;
	public GameObject muzzleEffect_Prefab;
	public float firePauseTime = 0.125f;
	public float errorAmount;
	public Transform[] muzzlePositions;
	public Transform rotatingPart;

	private Transform target;
	private float nextFireTime;
	private float nextMoveTime;
	private Quaternion desiredRotation;
	private float aimError;

		
	// Update is called once per frame
	void Update () 
	{
		if(target)
		{
			if(Time.time >= nextMoveTime)
			{
				CalculateAimPosition(target.position);
				rotatingPart.rotation = Quaternion.Lerp(rotatingPart.rotation, desiredRotation, Time.deltaTime * turnSpeed); 

			} 
			if(Time.time >= nextFireTime)
			{
				FireProjectile();
			}
		}
	}

	void FireProjectile()
	{
		//GetComponent<AudioSource>().Play();
		nextFireTime = Time.time + reloadTime;
		nextMoveTime = Time.time + firePauseTime;
		CalculateAimError();
		foreach(Transform muz in muzzlePositions)
		{
			Instantiate(projectile_Prefab, muz.position, muz.rotation);
			Instantiate(muzzleEffect_Prefab, muz.position, muz.rotation);
		}
	}

	void CalculateAimPosition(Vector3 targetPos)
	{
		Vector3 aimPoint = new Vector3(targetPos.x-rotatingPart.position.x+aimError, targetPos.y-rotatingPart.position.y+aimError, targetPos.z-rotatingPart.position.z+aimError);
		desiredRotation = Quaternion.LookRotation(aimPoint);
	}

	void CalculateAimError()
	{
		aimError = Random.Range(- errorAmount, errorAmount);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			nextFireTime = Time.time + (reloadTime * .5f);
			target = other.gameObject.transform;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.transform == target)
		{
			target = null;
		}
	}
}
