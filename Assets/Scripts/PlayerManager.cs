using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	private const string Player_Id_Prefix = "Player ";
	private static Dictionary<string ,GameObject> players = new Dictionary<string, GameObject>();

	public static void RegisterPlayer (string _netID, GameObject _player){
		string _playerId = Player_Id_Prefix + _netID;
		players.Add (_playerId, _player);
		_player.transform.name = _playerId;
	}

}
