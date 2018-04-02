using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileMouseOver : MonoBehaviour {
	public Camera cam;
	public LayerMask Tile;
	public Collider coll;
	// Use this for initialization
	void Start () {
		cam = PlayerSetup.playerCam;
		coll = GetComponent<Collider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("r")){
			//ray();
		}
	}

	void ray(){
		Ray ray = cam.ScreenPointToRay  (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100f, Tile.value)) {
			Renderer rend = hit.collider.GetComponent<Renderer> ();
			Texture2D texture = (Texture2D)rend.material.mainTexture;
			Vector2 pixelUV = hit.textureCoord;
			Debug.Log ("Hit Point : " + ((int)pixelUV.x * rend.material.mainTexture.width) + "--" + (int)(pixelUV.y * rend.material.mainTexture.height));

		}

	}
}
