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
	[SyncVar] public float lowestMinimumSpeed;

	public LayerMask type;
	protected NavMeshAgent enemy;



	private int waypointIndex = 1;
	private Transform wayPoints;
	private string _ID;


	// Use this for initialization
	void Start () {
		setup ();
		//Debug.Log ("waypoint size =" + WayPointManager.GetSize ());
		gameObject.tag = "Enemy";
		Type ();
		enemy = GetComponent<NavMeshAgent> ();
		FirstPoint ();
		Movement ();



		_ID = GetComponent<NetworkIdentity> ().netId.ToString();
		EnemyManager.RegisterEnemy (_ID, this.gameObject);


	}

	// Update is called once per frame
	void Update () {
		Vector3 dir = GetComponent<NavMeshAgent> ().destination - transform.position;
		transform.Translate (dir.normalized * speed*Time.deltaTime,Space.World);
		Vector3.Distance ( WayPointManager.GetWayPoints(waypointIndex), transform.position);
		enemy.speed = speed; //distance/time


		if(Vector3.Distance (this.transform.position, enemy.destination) <= 1.0f){
			GetNextWayPoint(); //Get next Ayyyy point

		}
	}
	void setup(){
		transform.name = "enemy ";
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

			Cmddeath ();
		} else {
			waypointIndex++;
			GetComponent<NavMeshAgent> ().destination =  WayPointManager.GetWayPoints(waypointIndex);
		}

	}	

	[Command]
	public void CmdChangeHP(int amount){
		if (!isServer) { //Nathan
			return;
		}
		hp = hp - amount  ;
		if (hp <= 0) {
			Cmddeath ();
			return;
		}

	}

	[Command]
	public void Cmddeath(){
		EnemyManager.DestroyEnemy (transform.name);
		Rpcdeath ();

		NetworkServer.Destroy (this.gameObject);

	}

	[ClientRpc]
	void Rpcdeath(){
		EnemyManager.DestroyEnemy (transform.name);

	}
}