using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {



	public static Camera sceneCamera;



	[SerializeField]
	public static Camera playerCam;

	[SerializeField]
	private string remoteLayerName = "RemotePlayer";

	[SerializeField]
	private Behaviour[] componentsToDisable;





	// Use this for initialization
	void Start () {
		if (!isLocalPlayer) {
			DisableComponents ();
			AssignRemotePlayer ();
			//playerCam.enabled = false;

		} 
		else {
			sceneCamera = Camera.main;
			if (sceneCamera != null) {
				sceneCamera.gameObject.SetActive (false);
			}
			playerCam = GetComponentInChildren<Camera> ();
		}

		RegisterPlayer ();
	}

	void Update () {
		if (!isLocalPlayer) {
			return;
		} 

	}
	//Give Each player a unique ID
	void RegisterPlayer(){
		string _ID = "Player " + GetComponent<NetworkIdentity> ().netId;
		transform.name = _ID;
	}

	//Disables components on remote players
	void DisableComponents(){
		if (!isLocalPlayer) {
			for (int i = 0; i < componentsToDisable.Length; i++) {
				componentsToDisable [i].enabled = false;
			}
		} 



	}

	//Assign layer to all Remote Players
	void AssignRemotePlayer(){
		gameObject.layer = LayerMask.NameToLayer (remoteLayerName);
	}


	void OnDisable(){
		if (sceneCamera != null) {
			sceneCamera.gameObject.SetActive (true);
		}
	}


}
