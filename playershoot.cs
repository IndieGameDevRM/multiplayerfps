using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
public class playershoot : NetworkBehaviour {
	public playerweapon weapon;
	[SerializeField]
	private Camera cam;
	private const string Player_Tag="Player";
	[SerializeField]
	private LayerMask mask;//find out by which obj ray has collide
	[SerializeField]
	private GameObject bullet;
	GameObject bulletclone;
	void Start () {
		if (cam == null) {
			Debug.LogError ("PlayerShoot:No camera referenced !");
			this.enabled = false;
		}
	}
	[Client]
	void shoot(){
		RaycastHit hit;
		if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,weapon.range,mask)){
			if (hit.collider.tag == Player_Tag) {
				CmdPlayerShot (hit.collider.name,weapon.damage);

			}
		}
			
	}
	[Command]

	void CmdPlayerShot(string player_ID,int Damage){
		Debug.Log (player_ID + " has been shot.");
		PlayerManager _player = GameManger.GetPlayer (player_ID);


		_player.RpcTakeDamage (Damage);
		//GameObject.Destroy (GameObject.Find(player_ID));
	}

	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			bulletclone =Instantiate (bullet, cam.transform.position, 
				cam.GetComponentInChildren<Transform>().rotation)as GameObject ;

			shoot ();
		} 
		Destroy (bulletclone, 4);
		
	}

}
