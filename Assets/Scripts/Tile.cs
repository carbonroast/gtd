using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Tile : NetworkBehaviour {
	[SyncVar] public bool canBuild;
	[SyncVar] public NetworkInstanceId parentNetId;
	[SyncVar] public string name;
	// Use this for initialization
	void Start () {
		if (!isServer) {
			return;
		}
		canBuild = true;

		GameObject parentObject = ClientScene.FindLocalObject (parentNetId);

		transform.SetParent (parentObject.transform);
		string _ID = "Cube " + GetComponent<NetworkIdentity> ().netId;
		transform.name = _ID;
		Debug.Log ("Name is : " + transform.name);
		TilesManager.RegisterTiles (transform.name, this.gameObject);
	
	}

	public void CanBuild(bool _canBuild){
		
		canBuild = _canBuild;
		Debug.LogError ("canbuild= " + canBuild);
	}



}
