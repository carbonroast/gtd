using UnityEngine;
public class TDMap  {

	public TDTile[,] _tiles;
	int width;
	int height;
	public TDTile tileType;
	public enum tile {
		Water,
		grassland,
		plains,
		mountain
	};



	public TDMap(int width, int height){
		this.width = width;
		this.height = height;

		_tiles = new TDTile[width , height];

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				_tiles [x, y] = new TDTile ();
				_tiles [x, y].type = (int)tile.mountain;
			}
		}



	}

	public TDTile GetTileAt(int x, int y){
		if (x < 0 || x >= width || y < 0 || y >= height) {
			Debug.Log ("Out of Bounds");
		}
		return  _tiles [x , y];
	}



}
