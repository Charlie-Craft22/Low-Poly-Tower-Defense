using UnityEngine;
using System.Collections;

public class SpecialRotationTurret : MonoBehaviour 
{
	public Transform projectile_Prefab;
	public float reloadTime = 1f;
	public float turnSpeed = 5f;
	//public float firePauseTime = 0.25f;
	public float errorAmount = 0.001f;
	public Transform[] muzzlePositions;
	public Transform pivot_Tilt, pivot_Pan, aim_Tilt, aim_Pan;

	private float nextFireTime;
	private Vector3 desiredRotation;
	private float aimRotation;

	private Transform target;


	void Update () 
	{

 

		if(target)
		{
			aim_Pan.LookAt(target);
			aim_Pan.eulerAngles = new Vector3(0, aim_Pan.eulerAngles.y, 0);
			aim_Tilt.LookAt(target);

			pivot_Pan.rotation = Quaternion.Lerp(pivot_Pan.rotation, aim_Pan.rotation, Time.deltaTime*turnSpeed);
			pivot_Tilt.rotation = Quaternion.Lerp(pivot_Tilt.rotation, aim_Tilt.rotation, Time.deltaTime*turnSpeed);

			if(Time.time >= nextFireTime)
			{
				FireProjectile();
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			nextFireTime = Time.time+(reloadTime*0.5f);
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

	void FireProjectile()
	{
		//GetComponent<AudioSource>().Play();
		nextFireTime = Time.time + reloadTime;

		int rand = Random.Range(0, muzzlePositions.Length);
        Transform lastProj = Instantiate(projectile_Prefab, muzzlePositions[rand].position, muzzlePositions[rand].rotation) as Transform;

        if (lastProj != null)
        {
            lastProj.gameObject.GetComponent<Projectile_Missle>().target = target;
            lastProj = null;
        }
        else
            Debug.Log("Nope");

        /*
        Projectile_Missle miss = proj.gameObject.GetComponent<Projectile_Missle>();
		if(miss!=null)
		{
			miss.target = target;
			Debug.Log("Happy");
		}
        */


    }

}
