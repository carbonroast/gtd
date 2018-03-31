using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class PlayerControls : NetworkBehaviour {

	public GameObject tower;





	public static bool newMouseOver;

	public GameObject BuilderPrefab;


	void Start(){
		newMouseOver = true;
	}   

	void Update () {
		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetKeyDown ("d")) {
			CmdSpawnBuilder ();
		}

	

	}

/*********************************************************** Command ************************************************/
	//Spawns the builder gameobject with authority
	[Command]
	void CmdSpawnBuilder(){
		GameObject go = Instantiate (BuilderPrefab);
		NetworkServer.SpawnWithClientAuthority (go,connectionToClient);
		string _ID = go.GetComponent<NetworkIdentity>().netId.ToString();
		go.transform.name = "Builder " + _ID;

	}
}


