
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour {
	public Matchsetting matchsetting;
	public static GameManger instance;
	private static Dictionary <string ,PlayerManager> player=new Dictionary<string, PlayerManager>();

	public static void RegisterPlayer(string _netID,PlayerManager _player){
		string _playerID = "Player" + _netID;
		player.Add (_playerID, _player);
		_player.transform.name = _playerID;

	}
	public static void UnRegisterPlayer (string _playerID){
		player.Remove (_playerID);
	}
	public static PlayerManager GetPlayer(string _playerID){
		return player [_playerID];
	}
	void Awake(){
		if (instance != null) {
			Debug.LogError ("More than one Gamemanager Scene");
		} else {
			instance = this;
		}
	}

}
