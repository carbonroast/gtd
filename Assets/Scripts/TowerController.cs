using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TowerController : NetworkBehaviour {


	public GameObject towerone;
	public GameObject towertwo;
	public LayerMask BuildBlock;

	private GameObject selectedTower;

	//private RaycastHit hit;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.Space)) {
			GetMousePosition ();
		}

	}

	//client only getting click
	//[Client]

	[Client]
	void GetMousePosition(){
		Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay (Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
		RaycastHit hit;
		Debug.Log (transform.name + " clicked " + Input.mousePosition);
		if (Physics.Raycast (ray, out hit, 100f, BuildBlock.value)) {
			CmdClick (hit.collider.name);
		}
	}
	[Command]
	void CmdClick(string tile){
		bool canBuild = TilesManager.GetTiles (tile).GetComponent<Tile>().canBuild;
		if (canBuild) {
			GameObject g = (GameObject)Instantiate (towerone);
			g.transform.position = TilesManager.GetTiles (tile).transform.position + towerone.transform.position;
			TilesManager.GetTiles (tile).GetComponent<Tile> ().canBuild = false;
			NetworkServer.Spawn (g);
			Debug.Log (" Has Built");
		} else {
			Debug.Log ("Cant Build There");
		}
	}
		

}

