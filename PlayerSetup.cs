using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] ComponentToDisable;
	Camera SceneCamera;
	[SerializeField]
	string remoteLayerName="RemotePlayer";

	void Start () {
		if (!isLocalPlayer) {
			DisableComponent ();
			AssignRemoteLayer ();

		} else {
			SceneCamera = Camera.main;
			if (SceneCamera != null) {
				SceneCamera.gameObject.SetActive (false);

			}
		}
		GetComponent<PlayerManager> ().SetUp ();
		RegisterID ();
	}
	public override void OnStartClient(){
		base.OnStartClient ();
		string _netID = GetComponent<NetworkIdentity> ().netId.ToString ();
		PlayerManager _player = GetComponent<PlayerManager> ();
		GameManger.RegisterPlayer (_netID, _player);
	}

	void RegisterID(){
		string _ID = "Player"+GetComponent<NetworkIdentity> ().netId;
		transform.name = _ID;
	}
	void AssignRemoteLayer(){
		gameObject.layer = LayerMask.NameToLayer (remoteLayerName);

	}
	void DisableComponent(){
		for (int i = 0; i < ComponentToDisable.Length; i++) {
			ComponentToDisable [i].enabled = false;
		}
	}
	void OnDisable(){


		if (SceneCamera != null) {
			SceneCamera.gameObject.SetActive (true);
		
		}
		GameManger.UnRegisterPlayer (transform.name);
	}



}
