using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Tile : NetworkBehaviour {
	[SyncVar] public bool canBuild;
	[SyncVar] public NetworkInstanceId parentNetId;
	[SyncVar] public string name;
	private WaitForSeconds m_StartWait;
	bool runOnce = true;


	// Use this for initialization
	void Start () {
		if (!isServer) {
			return;
		}


		canBuild = true;
		transform.name = "Cube ";
		string _ID = GetComponent<NetworkIdentity> ().netId.ToString();
		//TilesManager.RegisterTiles (_ID, this.gameObject);
		m_StartWait = new WaitForSeconds(3.0f);
		StartCoroutine (Setup(transform.name));

	}

	public void CanBuild(bool _canBuild){
		
		canBuild = _canBuild;

	}

/*********************************************************** Command ************************************************/
	[Command]
	public void CmdRename(string name){
		transform.name = name;

		RpcRename (name);
	}

/*********************************************************** ClientRpc ************************************************/
	[ClientRpc]
	void RpcRename(string name){
		transform.name = name;


	}

/*********************************************************** IEnumerator ************************************************/
	private IEnumerator Setup( string name)
	{
		//wait to be sure that all are ready to start
		yield return m_StartWait;
		CmdRename (name);
	}

}
