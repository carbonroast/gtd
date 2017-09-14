using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {



	//public GameObject projectilePrefab;
	//public GameObject tower;
	public float projectileSpeed;
	public bool isTrigger;

	[HideInInspector]
	public Transform target;
	public int damage;

	private Vector3 direction;
	private float distanceThisFrame;




	void Awake(){
		gameObject.GetComponent<BoxCollider>().isTrigger = isTrigger;

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
		Enemy e = target.GetComponent<Enemy>();
		e.ChangeHP(damage);
		Destroy (gameObject);
	}


}