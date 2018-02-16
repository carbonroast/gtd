using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

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

	//Spawns the builder gameobject with authority
	[Command]
	void CmdSpawnBuilder(){
		GameObject go = Instantiate (BuilderPrefab);



		NetworkServer.SpawnWithClientAuthority (go,connectionToClient);
	}



}

