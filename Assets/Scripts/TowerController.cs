using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TowerController : NetworkBehaviour {


	public GameObject towerone;
	public GameObject towertwo;
	public LayerMask Tile;

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

/*********************************************************** Client *************************************************/
	//client only getting click
	[Client]
	void GetMousePosition(){
		Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay (Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 30,Color.red, 30);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100f, Tile.value)) {
			//Graphic
			Vector3 location = hit.point;

			//Data
			Renderer rend = hit.collider.GetComponent<Renderer> ();
			Texture2D texture = (Texture2D)rend.material.mainTexture;
			Vector2 pixelUV = hit.textureCoord;
			Vector2 gridTile = new Vector2((float)(pixelUV.x * rend.material.mainTexture.width) / 16, (float)(pixelUV.y * rend.material.mainTexture.height) / 16);
			Debug.Log (gridTile.x + "-" + gridTile.y);
			CmdSpawnTower (hit.collider.name,location,gridTile);

		}

//
//
//
//			//Debug.Log (hit.point);
//

//		}
	}

	void CheckSurroundingTiles(string tileMap,Vector3 location, Vector2 pixelUV, Renderer rend){
		//Graphics
		Vector3[] buildCheckG = new [] {
			new Vector3 (location.x + .5f, 0f, location.z + .5f), 	//Top right
			new Vector3 (location.x - .5f, 0f, location.z + .5f), 	//Top Left
			new Vector3 (location.x + .5f, 0f, location.z - .5f), 	//Bottom Right
			new Vector3 (location.x - .5f, 0f, location.z - .5f) 	//Bottom Left
		};

		//Data
		Vector2[] buildCheckD = new [] {
			new Vector2 ((float)((pixelUV.x * rend.material.mainTexture.width) / 16) + .5f, (float)((pixelUV.y * rend.material.mainTexture.height) / 16) + .5f),	//Top right
			new Vector2 ((float)((pixelUV.x * rend.material.mainTexture.width) / 16) - .5f, (float)((pixelUV.y * rend.material.mainTexture.height) / 16) + .5f),	//Top Left
			new Vector2 ((float)((pixelUV.x * rend.material.mainTexture.width) / 16) + .5f, (float)((pixelUV.y * rend.material.mainTexture.height) / 16) - .5f),	//Bottom Right
			new Vector2 ((float)((pixelUV.x * rend.material.mainTexture.width) / 16) - .5f, (float)((pixelUV.y * rend.material.mainTexture.height) / 16) - .5f) 	//Bottom Left
		};

		//Graphics
		foreach (Vector3 surroundingTile in buildCheckG) {

		}

		//Data
		foreach (Vector2 surroundingTile in buildCheckD) {
			int x = Mathf.RoundToInt (Mathf.Floor (surroundingTile.x));
			int y = Mathf.RoundToInt (Mathf.Floor (surroundingTile.y));
			TDTile tile = TilesManager.GetTile (tileMap, x, y);
			Debug.Log ("tile[" + x + "][" + y + "].build is " + tile.build);
		}
	}




/*********************************************************** Command ************************************************/
	[Command]
	void CmdSpawnTower(string tileMap, Vector3 location, Vector2 gridTile){


		int x = Mathf.RoundToInt (Mathf.Floor (gridTile.x));
		int y = Mathf.RoundToInt (Mathf.Floor (gridTile.y));

		TDTile _tile = TilesManager.GetTile( tileMap, x,y);

		bool canBuild = _tile.build;
		if (canBuild) {
			GameObject go = (GameObject)Instantiate (towerone);

			go.transform.position = new Vector3(Mathf.Floor(location.x)+.5f,0.5f,Mathf.Floor(location.z)+.5f) + towerone.transform.position;


			_tile.build = false;
			NetworkServer.SpawnWithClientAuthority (go,connectionToClient);


			//Debug.Log ("TowerController : " + "Has Built");

		} else {
			Debug.Log ("Command : " + "Cant Build There");
		}

	}
		
}

