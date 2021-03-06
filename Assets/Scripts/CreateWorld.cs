﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWorld : MonoBehaviour {

	public GameObject buildPlace;
	public int xSize;
	public int ySize;

	void Start () {
		for (int i = 0; i < xSize; i++) {
			for (int j = 0; j < ySize; j++) {
				GameObject childObject = (GameObject)Instantiate (buildPlace);
				childObject.transform.parent = this.transform;
				childObject.transform.position = new Vector3 (i+(float).5, (float)-.5,j+(float).5);
				childObject.name = i + " " + j;
				childObject.AddComponent<CanBuild> ();
				childObject.layer = LayerMask.NameToLayer ("BuildBlock");

			}
		}
	}
}
