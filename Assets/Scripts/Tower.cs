using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]

public class Tower : NetworkBehaviour {

	[Header ("Values")]
	public int damage;
	public int mana;
	public float fireRate;
	public float range;
	//public List<string> canHit = new List<string> ();
	public GameObject projectile;
	public LayerMask canHit;


	[HideInInspector]
	public Collider[] targetQueue;
	public Transform firePoint;

	private float fireCountDown = 0f;

	void Start () {

		//canHit = LayerMask.NameToLayer ("Air");
		Setup();
		gameObject.GetComponent<SphereCollider> ().radius = range;
		gameObject.GetComponent<SphereCollider> ().isTrigger = true;


	}


	void Update () {

	}


	public virtual void Setup(){
		transform.name = "Tower ";

	}

	void OnTriggerStay(Collider other){
		if (!isClient) {
			return;
		}
		if(other.gameObject.tag == "Enemy"){
			//if frame rate bad, change to call 10 times a frame

			targetQueue = Physics.OverlapSphere(transform.position,range, canHit.value);

			if (fireCountDown <= 0f) {
				CmdAttack ();
				fireCountDown = 1f / fireRate;
			}
			fireCountDown -= Time.deltaTime;
		}	
	}

	public void RegisterTower(){
		string _ID = GetComponent<NetworkIdentity> ().netId.ToString();
		TowerManager.RegisterTower (_ID, this.gameObject);
		CmdRename (this.transform.name);
	}
/*********************************************************** Command ************************************************/
	[Command]
	public virtual void CmdAttack(){
		if(targetQueue.Length != 0){
			GameObject g = (GameObject)Instantiate(projectile, firePoint.position, Quaternion.identity);
			//GameObject g = (GameObject)Instantiate(GetComponent<Projectile>().projectilePrefab, firePoint.position, firePoint.rotation);
			g.GetComponent<Projectile>().target = targetQueue[0].transform;
			g.GetComponent<Projectile>().damage = damage;
			NetworkServer.Spawn (g);

		}
	}

	[Command]
	public void CmdRename(string name){
		transform.name = name;
		RpcRename (name);
	}



	[ClientRpc]
	void RpcRename(string name){
		transform.name = name;
	}

}