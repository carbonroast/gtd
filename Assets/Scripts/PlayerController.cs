using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
	public LayerMask canMove;
	public GameObject towerone;
	private Vector3 position;
	public LayerMask BuildBlock;
	protected NavMeshAgent player;
	public static Camera playerCam;

	public static bool newMouseOver;

	[HideInInspector]
	public RaycastHit hit;




	void Start(){
		newMouseOver = true;
		player = GetComponent<NavMeshAgent> ();
		playerCam = GetComponentInChildren<Camera> ();
		if (playerCam == null) {
			Debug.Log ("PlayerController: No camera referenced!");
			this.enabled = false;
		}
	}   

	void FixedUpdate () {
		if (!isLocalPlayer) {
			return;
		}
		CmdClick ();

	}

	//[Command]
	void CmdClick(){
		if (Input.GetMouseButtonDown (1)) {
			//Debug.Log ("down");
			Debug.Log (transform.name + "has clicked " + Input.mousePosition);
			Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay  (Input.mousePosition);
		

			if (Physics.Raycast (ray, out hit, 100f, canMove.value)) {
				Debug.Log ("position is " + hit.point);
				position = hit.point;
				//transform.position = position;

				player.destination = position;

			}
		}

	}
}

