
public class TDMap  {


	int size_x;
	int size_y;
	int[,] map_data;


	public TDMap(int size_x, int size_y){
		this.size_x = size_x;
		this.size_y = size_y;

		map_data = new int[size_x, size_y];


	}

	public int GetTileAt(int x, int y){
		return map_data [x, y];
	}

}
