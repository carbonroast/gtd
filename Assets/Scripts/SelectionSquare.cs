using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class SelectionSquare : MonoBehaviour {


	public List<string> _grid = new List<string>();

	void Awake(){
		GetComponent<BoxCollider>().isTrigger = true;
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Tile")){
			if (_grid.Count != 0) {
				for (int i = 0; i < _grid.Count; ++i) {
					if (other.name == _grid [i]) {
						return;
					}
						

				}
				_grid.Add(other.name);

			} else {
				_grid.Add(other.name);
			}

		}

	}

	public List<string> GetGrid(){
		return _grid;
	}

	void OnMouseOver(){

		if(PlayerControls.newMouseOver){
			//Debug.Log ("mouseOver");
			SelectBuildLocation();
		}
	}

	void OnMouseExit(){
		if (PlayerControls.newMouseOver == false) {
			for (int i = 0; i < _grid.Count; ++i) {
				TilesManager.GetTiles (_grid [i]).GetComponent<Renderer> ().material.SetColor ("_Color", Color.white);
			}

			PlayerControls.newMouseOver = true;
			//_grid = this.transform.parent.GetComponent<SelectionSquare>()._grid ;
			//Debug.Log (PlayerController.newMouseOver);
		}
	}

	void SelectBuildLocation(){
		PlayerControls.newMouseOver = false;
		for(int i=0; i < _grid.Count; ++i){
			TilesManager.GetTiles (_grid[i]).GetComponent<Renderer> ().material.SetColor ("_Color",Color.blue);
		}
		//Debug.Log (PlayerController.newMouseOver);
	}

}