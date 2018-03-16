using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Tile : NetworkBehaviour {
	[SyncVar] public bool canBuild;
	[SyncVar] public NetworkInstanceId parentNetId;
	[SyncVar] public string name;
	bool runOnce = true;


	// Use this for initialization
	void Start () {
		if (!isServer) {
			return;
		}
		//GameObject parentObject = ClientScene.FindLocalObject (parentNetId);.
		//transform.SetParent (parentObject.transform);
		//TilesManager.RegisterTiles (_ID, this.gameObject);

		canBuild = true;

		string _ID = GetComponent<NetworkIdentity> ().netId.ToString();
		transform.name = "Cube " + _ID;
		name = transform.name;

		//CmdRename does not run correctly in start
		//CmdRename (transform.name);

	}
//	void Update(){
//		if (!isServer) {
//			return;
//		}
//		if (runOnce) {
//			
//			CmdRename (transform.name);
//			runOnce = false;
//		}
//	}
//
	public void CanBuild(bool _canBuild){
		
		canBuild = _canBuild;
		Debug.LogError ("canbuild= " + canBuild);
	}

	[Command]
	public void CmdRename(string name){
		transform.name = name;
		Debug.Log ("CmdRename : Name is now " + name);
		RpcRename (name);
	}

	[ClientRpc]
	void RpcRename(string name){
		transform.name = name;
		Debug.Log ("RpcRename : Name is now " + name);

	}

}
