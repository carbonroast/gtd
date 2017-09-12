using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class Tower : MonoBehaviour {
	
	[Header ("Values")]
	public int damage;
	public int mana;
	public int fireRate;
	public int range;
	public List<string> canHit = new List<string> ();
	public GameObject projectile;

	[Header ("SetValues")]
	public List<GameObject> targetQueue = new List<GameObject> ();
	public Transform firePoint;


	void Start () {
		
	}
	

	void Update () {
		
	}
		

	public virtual void OnDrawGizmosSelected(){
		//Gizmos.color = Color.red;
		//Gizmos.DrawWireSphere (transform.position, range);
	}

	void OnTriggerEnter(Collider other){
		//GameObject g = (GameObject)Instantiate (bulletPrefab, transform.position, Quaternion.identity);
		//g.GetComponent<Bullet> ().target = co.transform;

		if(other.gameObject.tag == "Enemy"){
			Debug.Log("hit");
			CanHitCheck(other);

		}

	}
	void OnTriggerStay(Collider other){
		if (other != null) {
			//Debug.Log("Dequeue");
		} else {
			Debug.Log("Dequeue");
			//targetQueue.Dequeue ();		
		}
	}
	void OnTriggerExit(Collider other){
		//Debug.Log("Dequeue");
		//targetQueue.Dequeue ();
	}

	void CanHitCheck(Collider other){
		Enemy enemy = other.GetComponent<Enemy> ();
		foreach (string type in enemy.type){
			foreach(string targettype in canHit){
				if(type == targettype){
					Debug.Log ("queued");
					targetQueue.Add(other.gameObject);
				}
			}

		}

	}
	void Attack(){
		/*if(targetQueue != null){

			GameObject g = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.identity);
			Projectile projectile = g.GetComponent<Projectile>();
			if(projectile != null){
				FlightPath(targetQueue);
			}
		*/
		}
}
