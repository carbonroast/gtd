using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPoints : MonoBehaviour {

	private Vector3 currentTarget;
	private int i;
	public static List<Transform> destination = new List<Transform> ();
	// Use this for initialization
	void Start () {
		Path ();
	}

	void Path(){

		for (int i = 0; i < transform.childCount; i++) {
			destination.Add (transform.GetChild (i));
		}


	}
}
