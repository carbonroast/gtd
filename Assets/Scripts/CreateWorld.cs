using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class CreateWorld : MonoBehaviour {

	public GameObject tile;
	public GameObject selectionSquare;
	public GameObject tetherRange;
	public int xSize;
	public int ySize;

	void Start () {
		//CmdSpawnWorld ();
		SpawnSelectionSquare ();
		SpawnWorld();

		}

		//public override void OnStartServer(){

		void SpawnWorld(){
			GameObject _tiles = new GameObject();
			_tiles.transform.parent = this.transform;
			_tiles.name = "Tiles";
			for (int i = 0; i < xSize; i++) {
				for (int j = 0; j < ySize; j++) {
					GameObject childObject = (GameObject)Instantiate (tile);
					childObject.transform.parent = _tiles.transform;
					childObject.transform.position = new Vector3 (i+(float).5, (float)-.5,j+(float).5);
					childObject.name = i + " " + j;
					childObject.AddComponent<Tile> ();
					childObject.layer = LayerMask.NameToLayer ("Tile");
					TilesManager.RegisterTiles (childObject.name, childObject);

				}
			}
		}
		void SpawnSelectionSquare(){
			GameObject _selectionSquares = new GameObject();
			_selectionSquares.transform.parent = this.transform;
			_selectionSquares.name = "SelectionSquares";
			GameObject _tetherRanges = new GameObject();
			_tetherRanges.transform.parent = this.transform;
			_tetherRanges.name = "TetherRanges";
			for (int i = 0; i < xSize -1; i++) {
				for (int j = 0; j < ySize -1; j++) {
					//Create SelectionSquare
					GameObject _selectionSquare = (GameObject)Instantiate (selectionSquare, new Vector3(i+(float)1.0,0,j+(float)1.0),Quaternion.identity);
					_selectionSquare.transform.parent = _selectionSquares.transform;
					_selectionSquare.name = "SelectionSquare " + i + " " + j;
					_selectionSquare.AddComponent<SelectionSquare> ();
					//_selectionSquare.layer = LayerMask.NameToLayer ("SelectionSquare");
					TilesManager.RegisterTiles (_selectionSquare.name, _selectionSquare);

				/*
					//Create TetherRange as child of SelectionSquare
					GameObject _tetherRange = (GameObject)Instantiate (tetherRange, new Vector3(i+(float)1.0,0,j+(float)1.0),Quaternion.identity);
					_tetherRange.transform.parent = _tetherRanges.transform;
					_tetherRange.AddComponent<TetherRange> ();
					_tetherRange.name = "TetherRange " + i + " " + j;
					_tetherRange.GetComponent<TetherRange> ().selectionSquare = TilesManager.GetTiles (_selectionSquare.name);
				*/
				}
			}
		}


	}