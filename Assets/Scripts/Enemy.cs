using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]

public class Enemy : MonoBehaviour {

	public int hp;
	public int mana;
	public int speed;
	protected NavMeshAgent enemy;
	//public LayerMask type;
	//public List<string> type = new List<string>();

	//public GameObject character;

	private int wavepointIndex = 1;




	// Use this for initialization
	void Start () {
		gameObject.tag = "Enemy";
		Type ();
		enemy = GetComponent<NavMeshAgent> ();
		FirstPoint ();
		Movement ();
	}

	// Update is called once per frame
	void Update () {
		//Vector3 dir = GetComponent<NavMeshAgent> ().destination - transform.position;
		//transform.Translate (dir.normalized * speed*Time.deltaTime,Space.World);
		//Vector3.Distance (destination[i].transform.position, transform.position);
		//currentTarget = destination[i].transform.position;
		//enemy.speed = speed;


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
		enemy.destination = WayPoints.destination [wavepointIndex].transform.position;
	}
	void GetNextWayPoint(){
		if (wavepointIndex >= WayPoints.destination.Count - 1) {
			Destroy (gameObject);
		} else {
			wavepointIndex++;
			GetComponent<NavMeshAgent> ().destination = WayPoints.destination [wavepointIndex].transform.position;
		}

	}	

	public void ChangeHP(int amount){
		hp = hp - amount;
		if (hp <= 0) {
			Destroy (gameObject);
			return;
		}

	}
}