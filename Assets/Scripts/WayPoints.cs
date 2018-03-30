using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class WayPoints : NetworkBehaviour {

	private Vector3 currentTarget;

	// Use this for initialization
	void Start () {
		if (!isServer) {
			return;
		}
		Path ();
	}

	void Path(){
		for (int i = 0; i < transform.childCount; i++) {
			WayPointManager.RegisterWayPoints (transform.GetChild (i).transform.position);
		}
	}
}
