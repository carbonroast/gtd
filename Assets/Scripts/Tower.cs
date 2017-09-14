using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class Tower : MonoBehaviour {

	[Header ("Values")]
	public int damage;
	public int mana;
	public float fireRate;
	public float range;
	//public List<string> canHit = new List<string> ();
	public GameObject projectile;
	//public LayerMask canHit;



	public Collider[] targetQueue;
	public Transform firePoint;
	[HideInInspector]
	private float fireCountDown = 0f;

	void Start () {
		//canHit = LayerMask.NameToLayer ("Air");
		range = gameObject.GetComponent<SphereCollider> ().radius;


	}


	void Update () {

	}




	void OnTriggerStay(Collider other){
		if(other.gameObject.tag == "Enemy"){
			//if frame rate bad, change to call 10 times a frame

			targetQueue = Physics.OverlapSphere(transform.position,range, 1 << 9);

			if (fireCountDown <= 0f) {
				Attack ();
				fireCountDown = 1f / fireRate;
			}
			fireCountDown -= Time.deltaTime;
		}	
	}



	public virtual void Attack(){
		if(targetQueue.Length != 0){
			GameObject g = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
			//GameObject g = (GameObject)Instantiate(GetComponent<Projectile>().projectilePrefab, firePoint.position, firePoint.rotation);
			g.GetComponent<Projectile>().target = targetQueue[0].transform;
			g.GetComponent<Projectile>().damage = damage;
		}
	}
}