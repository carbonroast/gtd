using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Unit : NetworkBehaviour {
	
	public LayerMask canMove;


	protected NavMeshAgent unit;


	public Camera cam;

	[HideInInspector]
	public RaycastHit hit;

	// Use this for initialization
	void Start () {
		unit = GetComponent<NavMeshAgent> ();
		//Get Camera from PlayerSetup used for movement
		cam = PlayerSetup.playerCam;
		Debug.Log ("spawned");
	}
	
	// Update is called once per frame
	void Update () {
		if (hasAuthority==false) {
			return;
		}

		clickToMove ();
	}
	//Shoots a ray which hits a buildblock and asks if it can move there
	void clickToMove(){
		if(Input.GetKeyDown("p")){
			Debug.Log("playerObjectCam : " + cam.transform.position);
		}
		if (Input.GetMouseButtonDown (1)) {
			//Debug.Log ("down");
			Debug.Log (transform.name + "has clicked " + Input.mousePosition);
			//	Debug.Log ("width - " + Screen.width + " height - " + Screen.height);

			Ray ray = cam.ScreenPointToRay  (Input.mousePosition);

			if (Physics.Raycast (ray, out hit, 100f, canMove.value)) {
				Debug.Log ("position is " + hit.point);
				Debug.Log (hit.collider.name);
				Vector3 position = hit.point;
				unit.destination = position;


			}
			Debug.DrawLine (cam.transform.position, hit.point, Color.red,20);
		}

	}


}
