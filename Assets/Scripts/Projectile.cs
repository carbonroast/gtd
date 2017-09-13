using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {



	//public GameObject projectilePrefab;
	//public GameObject tower;
	public float projectileSpeed;
	public bool isTrigger;
	public Transform target;
	private Vector3 direction;
	private float distanceThisFrame;

	void start(){

	}

	void FixedUpdate(){
		
		if(target != null){
			direction = target.position - transform.position;
			distanceThisFrame = projectileSpeed * Time.deltaTime;
			if(direction.magnitude <= distanceThisFrame){
				TargetHit();
				return;
			}
			transform.Translate (direction.normalized * distanceThisFrame, Space.World);

		}
		else{
			Destroy(gameObject);
			return;
		}

	}

	public virtual void TargetHit(){
		Debug.Log ("hit");
		//Destroy(gameObject);
		//Enemy e = target.GetComponent<Enemy>();
		//e.ChangeHP(10);
	}


}