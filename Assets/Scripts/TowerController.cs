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
		if (!isLocalPlayer) {
			return;
		}
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetKeyDown ("space")) {
			GetMousePosition ();
		}

	}

	//client only getting click
	[Client]
	void GetMousePosition(){
		Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay (Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 30,Color.red, 30);
		RaycastHit hit;
		//Debug.Log ("TowerController : " + transform.name + " clicked " + Input.mousePosition);
		if (Physics.Raycast (ray, out hit, 100f, BuildBlock.value)) {
			Debug.Log ("TowerController : " + "Has hit " + hit.collider.name);
			CmdSpawnTower (hit.collider.name);
		}
	}

/*********************************************************** Command ************************************************/
	[Command]
	void CmdSpawnTower(string tile){
		GameObject _go = TilesManager.GetTiles (tile);

		bool canBuild =_go.GetComponent<Tile>().canBuild;
		if (canBuild) {
			GameObject go = (GameObject)Instantiate (towerone);
			go.transform.position = TilesManager.GetTiles (tile).transform.position + towerone.transform.position;


			TilesManager.GetTiles (tile).GetComponent<Tile> ().canBuild = false;
			NetworkServer.SpawnWithClientAuthority (go,connectionToClient);


			//Debug.Log ("TowerController : " + "Has Built");

		} else {
			Debug.Log ("Command : " + "Cant Build There");
		}

	}
		
}

