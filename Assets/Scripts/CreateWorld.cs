using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class CreateWorld : NetworkBehaviour {

	public GameObject tile;
	public GameObject selectionSquare;
	public GameObject tetherRange;
	public int xSize;
	public int ySize;
	private WaitForSeconds m_StartWait;

	 void Start () {
		if (!isServer) {
			return;
		}
		//RpcSpawnSelectionSquare ();
		//m_StartWait = new WaitForSeconds(3.0f);
		//StartCoroutine (BuildMap());
		CmdSpawnWorld();
	}


	[Command]
	public void CmdSpawnWorld(){
		for (int i = 0; i < xSize; i++) {
			for (int j = 0; j < ySize; j++) {
				GameObject childObject = (GameObject)Instantiate (tile);
				//childObject.GetComponent<Tile> ().parentNetId = this.netId;
				//childObject.GetComponent<Tile>().name = i + " " + j;
				//childObject.transform.parent = this.transform;
				childObject.transform.position = new Vector3 (i+(float).5, (float)-.5,j+(float).5);
				//childObject.name = childObject.GetComponent<Tile>().name;
				//Debug.Log ("CreateWorld: Creating " + childObject.name);


				NetworkServer.Spawn (childObject);
				string _ID = childObject.GetComponent<NetworkIdentity> ().netId.ToString();
				Debug.Log (_ID);
				TilesManager.RegisterTiles (_ID, childObject.gameObject);
				//RpcNameTiles (childObject,_ID);
			}
		}
	
		print ("CmdSpawnWorld : World Created");
	}
	private IEnumerator BuildMap()
	{
		CmdSpawnWorld ();
		//wait to be sure that all are ready to start
		yield return m_StartWait;

		// Start off by running the 'RoundStarting' coroutine but don't return until it's finished.

	}
//
//	[ClientRpc]
//	void RpcNameTiles(GameObject childObject ,string TileNetID){
//		
//		childObject.transform.name = "Cube " + TileNetID;
//		print ("RpcNameTiles : " + childObject.transform.name + " Created");
//	}
//






//		void RpcSpawnSelectionSquare(){
//			GameObject _selectionSquares = new GameObject();
//			_selectionSquares.transform.parent = this.transform;
//			_selectionSquares.name = "SelectionSquares";
//			GameObject _tetherRanges = new GameObject();
//			_tetherRanges.transform.parent = this.transform;
//			_tetherRanges.name = "TetherRanges";
//			for (int i = 0; i < xSize -1; i++) {
//				for (int j = 0; j < ySize -1; j++) {
//					//Create SelectionSquare
//					GameObject _selectionSquare = (GameObject)Instantiate (selectionSquare, new Vector3(i+(float)1.0,0,j+(float)1.0),Quaternion.identity);
//					_selectionSquare.transform.parent = _selectionSquares.transform;
//					_selectionSquare.name = "SelectionSquare " + i + " " + j;
//					_selectionSquare.AddComponent<SelectionSquare> ();
//					//_selectionSquare.layer = LayerMask.NameToLayer ("SelectionSquare");
//					TilesManager.RegisterTiles (_selectionSquare.name, _selectionSquare);
//					NetworkServer.Spawn (_selectionSquare);
//				/*
//					//Create TetherRange as child of SelectionSquare
//					GameObject _tetherRange = (GameObject)Instantiate (tetherRange, new Vector3(i+(float)1.0,0,j+(float)1.0),Quaternion.identity);
//					_tetherRange.transform.parent = _tetherRanges.transform;
//					_tetherRange.AddComponent<TetherRange> ();
//					_tetherRange.name = "TetherRange " + i + " " + j;
//					_tetherRange.GetComponent<TetherRange> ().selectionSquare = TilesManager.GetTiles (_selectionSquare.name);
//				*/
//				}
//			}
//		}


}