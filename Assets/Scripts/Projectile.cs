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

	public Vector3 direction;
	public float distanceThisFrame;
    public Vector3 lastPostion;



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
            lastPostion = target.position;
			direction = target.position - transform.position;
			distanceThisFrame = projectileSpeed * Time.deltaTime;
			transform.Translate (direction.normalized * distanceThisFrame, Space.World);

			/*possible ground targeting
			if(direction.magnitude <= distanceThisFrame && target != null){
				
				return;
			}*/


		}
		else{
            direction = lastPostion - transform.position;
            distanceThisFrame = projectileSpeed * Time.deltaTime;
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
            if(Mathf.Abs( direction.x) <= 0.1f && Mathf.Abs(direction.z) <= 0.1f )
            {
                //Debug.Log("object dissapperad x: " + Mathf.Abs(direction.x )+ " z :" + Mathf.Abs(direction.z));
                NetworkServer.Destroy(gameObject);
            }
		}

	}
	void OnTriggerEnter(Collider other){
		//Debug.Log ("trigger");
		if (other.transform == target) {
			TargetHit();
			//Debug.Log ("HIT");
		}
	}
	public virtual void Shooting(){

	}

	public virtual void TargetHit(){

		//Debug.Log ("trigger");
		//Destroy(gameObject);
		Enemy e = target.GetComponent<Enemy>();
		e.CmdChangeHP(damage);
		NetworkServer.Destroy(this.gameObject);
	}

/*********************************************************** Command ************************************************/
	[Command]
	public void CmdRename(string name){
		transform.name = name;
		RpcRename (name);
	}

/*********************************************************** ClientRpc ************************************************/
	[ClientRpc]
	void RpcRename(string name){
		transform.name = name;
	}


}