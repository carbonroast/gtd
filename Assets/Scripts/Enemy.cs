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
	private Vector3 bestGuessPosition;

	// Use this for initialization
	void Start () {
		if (!isServer) {
			return;
		}
		setup ();
		//Debug.Log ("waypoint size =" + WayPointManager.GetSize ());
		gameObject.tag = "Enemy";
		Type ();
		enemy = GetComponent<NavMeshAgent> ();
		FirstPoint ();
		Movement ();



		string _ID = GetComponent<NetworkIdentity> ().netId.ToString();
		EnemyManager.RegisterEnemy (_ID, this.gameObject);
		CmdRename (transform.name);

	}

	// Update is called once per frame
	void FixedUpdate () {
		
		bestGuessPosition = bestGuessPosition + (GetComponent<NavMeshAgent> ().velocity * Time.deltaTime);
		//transform.position = Vector3.Lerp (transform.position, bestGuessPosition, Time.deltaTime);


		if (!isServer) {
			
			return;
		}

		Vector3 dir = GetComponent<NavMeshAgent> ().destination - transform.position;
		transform.Translate (dir.normalized * speed*Time.deltaTime,Space.World);
		float distanceLeft = Vector3.Distance (this.transform.position, enemy.destination);
		enemy.speed = speed; 
		//Debug.Log (enemy.speed);

		if(distanceLeft <= 1.2f){
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
		enemy.acceleration = 100;
	}
	void FirstPoint(){
		enemy.destination = WayPointManager.GetWayPoints(waypointIndex);
	}
	void GetNextWayPoint(){
		if (waypointIndex >= WayPointManager.GetSize()-1) {
			Cmddeath ();
		} 
		else {
			waypointIndex++;
			GetComponent<NavMeshAgent> ().destination =  WayPointManager.GetWayPoints(waypointIndex);
		}

	}	
/*********************************************************** Command ************************************************/
	[Command]
	public void CmdRename(string name){
		transform.name = name;
		RpcRename (name);
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



	}
/*********************************************************** ClientRpc ************************************************/
	[ClientRpc]
	void RpcRename(string name){
		transform.name = name;
	}

	[ClientRpc]
	void Rpcdeath(){
		EnemyManager.DestroyEnemy (transform.name);
		NetworkServer.Destroy (this.gameObject);
	}

}