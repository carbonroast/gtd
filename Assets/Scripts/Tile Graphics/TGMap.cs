using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TGMap : NetworkBehaviour {

	public int size_x= 100;
	public int size_z = 50;
	public float tileSize = 1.0f;

	public Texture2D terrainTiles;
	public int tileResolution;
	public TDMap map;
	// Use this for initialization
	void Start () {
		if (!isServer) {
			return;
		}
		BuildMesh ();
	}


	public void BuildMesh(){



		int numTIles = size_x * size_z;
		int numTris = numTIles * 2;

		int vsize_x = size_x + 1;
		int vsize_z = size_z + 1;
		int numVerts = vsize_x * vsize_z;

		Vector3[] vertices = new Vector3[ numVerts];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];

		int[] triangles = new int[ numTris * 3];

		int x, z;
		for (z = 0; z < vsize_z; z++) {
			for (x = 0; x < vsize_x; x++) {
				vertices [z * vsize_x + x] = new Vector3 (x * tileSize, 0, -z * tileSize);
				normals [z * vsize_x + x] = Vector3.up;
				uv [z * vsize_x + x] = new Vector2 ((float)x / size_x, 1f - (float)z / size_z);
			}
		}

		for (z = 0; z < size_z; z++) {
			for (x = 0; x < size_x; x++) {
				int squareIndex = z * size_x + x;
				int triOffSet = squareIndex * 6;
				triangles [triOffSet + 0] = z * vsize_x + x +			0;
				triangles [triOffSet + 2] = z * vsize_x + x + vsize_x + 0;
				triangles [triOffSet + 1] = z * vsize_x + x + vsize_x +	1;

				triangles [triOffSet + 3] = z * vsize_x + x +			0;
				triangles [triOffSet + 5] = z * vsize_x + x + vsize_x + 1;
				triangles [triOffSet + 4] = z * vsize_x + x + 			1;
			}
		}

		Mesh mesh = new Mesh ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;

		MeshFilter mesh_filter = GetComponent<MeshFilter> ();
		MeshCollider mesh_collider = GetComponent<MeshCollider> ();

		mesh_filter.mesh = mesh;
		mesh_collider.sharedMesh = mesh;

		BuildTexture ();
	}

	void BuildTexture(){
		
		map = new TDMap (size_x, size_z);

		//string _ID = GetComponent<NetworkIdentity> ().netId.ToString();
		//TilesManager.RegisterEnemy (_ID, this.gameObject);
		//CmdRename (transform.name);



		int texWidth = size_x * tileResolution;
		int texHeight = size_z * tileResolution;
		Texture2D texture = new Texture2D (texWidth, texHeight);


		Color[][] tiles = ChopUpTiles ();

		for (int y = 0; y < size_z; y++) {
			for (int x = 0; x < size_x; x++) {
				Color[] p = tiles [map.GetTileAt(x,y).type];
				texture.SetPixels (x * tileResolution, y * tileResolution, tileResolution, tileResolution, p);
			}

		}

		Color[] pa = tiles [1];
		texture.SetPixels (3 * tileResolution, 3 * tileResolution, tileResolution, tileResolution, pa);
		map.GetTileAt (3, 3).type = 1;

		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.Apply ();

		MeshRenderer mesh_renderer = GetComponent<MeshRenderer> ();
		mesh_renderer.sharedMaterials[0].mainTexture = texture;

		string _ID = GetComponent<NetworkIdentity> ().netId.ToString();
		this.transform.name = "MapTile " + _ID;
		TilesManager.RegisterMap (_ID, this.gameObject);

		Debug.Log ("Texture Finished!");
	}

	Color[][] ChopUpTiles(){
		int numTilesPerRow = terrainTiles.width / tileResolution;
		int numRows = terrainTiles.height / tileResolution;

		Color[][] tiles = new Color[numTilesPerRow * numRows][];

		for (int y = 0; y < numRows; y++) {
			for (int x = 0; x < numTilesPerRow; x++) {
				tiles [y * numTilesPerRow + x] = terrainTiles.GetPixels (x * tileResolution, y * tileResolution, tileResolution, tileResolution);
			}
		}

		return tiles;
	}

}
