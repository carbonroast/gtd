using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
	//public GameObject player;
	private Vector3 position;
	protected NavMeshAgent player;
	public LayerMask canMove;
	// Use this for initialization
	void Start () {
		player = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		Click ();
	}
	void Click(){
		if (Input.GetMouseButtonDown (0)) {
			//Debug.Log ("down");
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100f, canMove.value)) {
				Debug.Log ("position is " + hit.point);
				position = hit.point;
				//transform.position = position;

				player.destination = position;

			}
		}

	}

}
