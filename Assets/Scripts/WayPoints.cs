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
	void Awake(){
		

	}
	// Update is called once per frame
	void Update () {
		/*
		float dist = Vector3.Distance (destination[i].transform.position, transform.position);
		currentTarget = destination[i].transform.position;

		if(dist < 2){
			if(i < destination.Count -1){
				i++;
				GetComponent<NavMeshAgent> ().destination = destination [i].transform.position;
			}
		}*/
	}
	void Path(){
		i = 1;
		for (int i = 0; i < transform.childCount; i++) {
			destination.Add (transform.GetChild (i));
		}
		//GetComponent<NavMeshAgent> ().destination = destination [i].transform.position;

	}
}
