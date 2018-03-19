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

		string _ID = GetComponent<NetworkIdentity> ().netId.ToString();
		transform.name = "Cube " + _ID;
		m_StartWait = new WaitForSeconds(3.0f);
		StartCoroutine (Setup(transform.name));

	}

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
	private IEnumerator Setup( string name)
	{
		
		//wait to be sure that all are ready to start
		yield return m_StartWait;
		CmdRename (name);
		// Start off by running the 'RoundStarting' coroutine but don't return until it's finished.

	}

}
