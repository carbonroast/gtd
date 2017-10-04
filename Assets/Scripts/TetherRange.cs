using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class TetherRange : MonoBehaviour {

	public GameObject selectionSquare;


	void Awake(){
		GetComponent<BoxCollider>().isTrigger = true;

	}

	void OnMouseEnter(){
		Debug.Log ("Eneter");
	}

	void OnMouseExit(){
		DeselectBuildLocation();
	}

	void DeselectBuildLocation(){
		Debug.Log ("MouseEXIT");
		if (PlayerController.newMouseOver == false) {
			
			for (int i = 0; i < selectionSquare.GetComponent<SelectionSquare> ()._grid.Count; ++i) {
				TilesManager.GetTiles (selectionSquare.GetComponent<SelectionSquare> ()._grid [i]).GetComponent<Renderer> ().material.SetColor ("_Color", Color.white);
			}

			PlayerController.newMouseOver = true;
			//_grid = this.transform.parent.GetComponent<SelectionSquare>()._grid ;
			Debug.Log (PlayerController.newMouseOver);
		}
	}
}