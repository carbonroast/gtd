using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;


public class Enemy : NetworkBehaviour {
	
	[SyncVar] public int hp;
	[SyncVar] public int mana;
	[SyncVar] public int damage;
	[SyncVar] public float speed;
	[SyncVar] public float minimumSpeed;

	public LayerMask type;
	protected NavMeshAgent enemy;



	private int waypointIndex = 1;
	private Transform wayPoints;



	// Use this for initialization
	void Start () {
		Debug.Log ("waypoint size =" + WayPointManager.GetSize ());
		gameObject.tag = "Enemy";
		Type ();
		enemy = GetComponent<NavMeshAgent> ();
		FirstPoint ();
		Movement ();
	}

	// Update is called once per frame
	void Update () {
		Vector3 dir = GetComponent<NavMeshAgent> ().destination - transform.position;
		transform.Translate (dir.normalized * speed*Time.deltaTime,Space.World);
		Vector3.Distance ( WayPointManager.GetWayPoints(waypointIndex), transform.position);
		enemy.speed = speed;


		if(Vector3.Distance (this.transform.position, enemy.destination) <= 1.0f){
			GetNextWayPoint();

		}
	}
	public virtual void Type(){

		gameObject.layer = LayerMask.NameToLayer ("Air");
	}
	public virtual void Movement(){
		enemy.acceleration = 60;
	}
	void FirstPoint(){
		enemy.destination = WayPointManager.GetWayPoints(waypointIndex);
	}
	void GetNextWayPoint(){
		if (waypointIndex >= WayPointManager.GetSize()-1) {
			Destroy (this.gameObject);
		} else {
			waypointIndex++;
			Debug.Log ("index =" + waypointIndex);
			GetComponent<NavMeshAgent> ().destination =  WayPointManager.GetWayPoints(waypointIndex);
		}

	}	

	public void ChangeHP(int amount){
		if (!isServer) {
			return;
		}
		hp = hp - amount;
		if (hp <= 0) {
			Destroy (gameObject);
			return;
		}

	}
}