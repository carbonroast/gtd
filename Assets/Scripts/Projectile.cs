using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour {



	//public GameObject projectilePrefab;
	//public GameObject tower;
	public float projectileSpeed;
	public bool isTrigger;

	[HideInInspector]
	public Transform target;
	public int damage;

	private Vector3 direction;
	private float distanceThisFrame;




	void Start(){
		if (!isServer) {
			return;
		}
		gameObject.GetComponent<BoxCollider>().isTrigger = isTrigger;
		string _ID = GetComponent<NetworkIdentity> ().netId.ToString();
		transform.name = "projectile ";
		ProjectileManager.RegisterProjectile (_ID, this.gameObject);
		CmdRename (transform.name);

	}

	void FixedUpdate(){
		if (!isServer) {
			return;
		}
		if(target != null){
			direction = target.position - transform.position;
			distanceThisFrame = projectileSpeed * Time.deltaTime;
			if(direction.magnitude <= distanceThisFrame && target != null){
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
		if (!isServer) {
			return;
		}
		//Debug.Log ("hit");
		//Destroy(gameObject);
		Enemy e = target.GetComponent<Enemy>();
		e.CmdChangeHP(damage);
		gameObject.SetActive(false);
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