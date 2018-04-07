using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TowerController : NetworkBehaviour {


	public GameObject towerone;
	public GameObject towertwo;
	public LayerMask Tile;

	[SerializeField]
	private GameObject previewGrid;
	[SerializeField]
	private GameObject previewTower;
	private GameObject selectedTower;

	private State state;
	private RaycastHit mousePosition;
	private GameObject pt;
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

		state = State.Idle;
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetKeyDown ("space")) {
			state = State.Building;
			pt = Instantiate (previewTower);

			GameObject pgParent = new GameObject ();
			pgParent.transform.parent = pt.transform;

			SetAlpha (pt.GetComponent<Renderer> ().material, .3f);

			GameObject tr =(GameObject)Instantiate(previewGrid,new Vector3(.5f, -.4f, .5f),Quaternion.identity,pgParent.transform);
			GameObject tl =(GameObject)Instantiate(previewGrid,new Vector3(-.5f, -.4f,.5f),Quaternion.identity,pgParent.transform);
			GameObject br =(GameObject)Instantiate(previewGrid,new Vector3(.5f,-.4f,-.5f),Quaternion.identity,pgParent.transform);
			GameObject bl =(GameObject)Instantiate(previewGrid,new Vector3(-.5f, -.4f,-.5f),Quaternion.identity,pgParent.transform);


		}
		if (state == State.Building) {
			
			BuildProcess (pt);
		}

	}

	void SetAlpha (Material material, float value){
		Color color = material.color;
		color.a = value;
		material.color = color;
	}



/*********************************************************** Client *************************************************/
	[Client]
	void BuildProcess(GameObject tower){
		Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay (Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 30,Color.red, 30);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100f, Tile.value)) {


			string tileMap = hit.collider.name;
			Vector3 location = hit.point;
			Renderer rend = hit.collider.GetComponent<Renderer> ();
			Texture2D texture = (Texture2D)rend.material.mainTexture;
			
			tower.transform.position = new Vector3(Mathf.RoundToInt(location.x),0.5f,Mathf.RoundToInt(location.z));
			Vector2 pixelUV = hit.textureCoord;
			
			//Graphics
			Vector3[] buildCheckG = new [] {
				new Vector3 (location.x + .5f, 0f, location.z + .5f), 	//Top right
				new Vector3 (location.x - .5f, 0f, location.z + .5f), 	//Top Left
				new Vector3 (location.x + .5f, 0f, location.z - .5f), 	//Bottom Right
				new Vector3 (location.x - .5f, 0f, location.z - .5f) 	//Bottom Left
			};

			Vector2[] buildCheckD = new [] {
				new Vector2 ((float)((pixelUV.x * rend.material.mainTexture.width) / 16) + .5f, (float)((pixelUV.y * rend.material.mainTexture.height) / 16) + .5f),	//Top right
				new Vector2 ((float)((pixelUV.x * rend.material.mainTexture.width) / 16) - .5f, (float)((pixelUV.y * rend.material.mainTexture.height) / 16) + .5f),	//Top Left
				new Vector2 ((float)((pixelUV.x * rend.material.mainTexture.width) / 16) + .5f, (float)((pixelUV.y * rend.material.mainTexture.height) / 16) - .5f),	//Bottom Right
				new Vector2 ((float)((pixelUV.x * rend.material.mainTexture.width) / 16) - .5f, (float)((pixelUV.y * rend.material.mainTexture.height) / 16) - .5f) 	//Bottom Left
			};

			for (int i = 0; i < tower.transform.GetChild(tower.transform.childCount-1).childCount; i++) {
				int x = Mathf.RoundToInt (Mathf.Floor (buildCheckD [i].x));
				int y = Mathf.RoundToInt (Mathf.Floor (buildCheckD [i].y));
				bool highlightColor = TilesManager.GetTile (tileMap, x, y).build;
				if (highlightColor) {
					//Green
					//Color color = renderer.
					tower.transform.GetChild(tower.transform.childCount-1).GetChild(i).GetComponent<Renderer>().material.color = new Color(0f,1f,0f,.4f);
				} else {
					//Red
					tower.transform.GetChild(tower.transform.childCount-1).GetChild(i).GetComponent<Renderer>().material.color = new Color(1f,0f,0f,.4f);
				}
			}

			if (Input.GetMouseButton (0)) {
				bool canBuild = true;
				foreach (Vector2 surroundingTile in buildCheckD) {
					int x = Mathf.RoundToInt (Mathf.Floor (surroundingTile.x));
					int y = Mathf.RoundToInt (Mathf.Floor (surroundingTile.y));
					TDTile tile = TilesManager.GetTile (tileMap, x, y);
					if (tile.build == false) {
						canBuild = false;
					}
				}
				if (canBuild) {
					CmdSpawnTower ( location);
					foreach (Vector2 surroundingTile in buildCheckD) {
						int x = Mathf.RoundToInt (Mathf.Floor (surroundingTile.x));
						int y = Mathf.RoundToInt (Mathf.Floor (surroundingTile.y));
						TDTile tile = TilesManager.GetTile (tileMap, x, y);
						tile.build = false;
						state = State.Idle;
					}
					Destroy (tower.gameObject);
				} else {

				}

			}
		}
	}
		


/*********************************************************** Command ************************************************/
	[Command]
	void CmdSpawnTower(Vector3 location){

		selectedTower  = Instantiate (towerone);

		selectedTower.transform.position = new Vector3(Mathf.RoundToInt(location.x),0.5f,Mathf.RoundToInt(location.z));

		NetworkServer.SpawnWithClientAuthority (selectedTower,connectionToClient);

		//selectedTower.GetComponent<Tower> ().RegisterTower ();

	}
		
}

