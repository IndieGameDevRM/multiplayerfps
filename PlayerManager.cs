using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerManager :NetworkBehaviour {
	#region playertracker
	[SerializeField]
	private int maxhealth = 100;
	[SyncVar]
	private int currentHealth;
	[SyncVar]
	private bool _isDead = false;
	[SerializeField]
	private Behaviour[] disableOnDealth;
	private bool[] wasEnabled;
	public bool isDead{
		get{ return _isDead;}
		protected set {_isDead = value;}
	}
	public void SetUp(){
		wasEnabled = new bool[disableOnDealth.Length];
		for (int i = 0; i < wasEnabled.Length; i++) {
			wasEnabled [i] = disableOnDealth [i].enabled;
		}
		setDefault ();

	}	private IEnumerator Respawn(){
		yield return new WaitForSeconds (GameManger.instance.matchsetting.respawnTime);
		setDefault ();
		Transform spawn_point = NetworkManager.singleton.GetStartPosition ();
		transform.position = spawn_point.position;
		transform.rotation = spawn_point.rotation;
	}

	private void Die(){
		isDead = true;
		//Disable component
		for(int i=0;i<disableOnDealth.Length;i++){
			disableOnDealth[i].enabled=false;
		}
		Collider col=GetComponent<Collider>();
		if(col!=null){
			col.enabled = false;
		}

		Debug.Log(transform.name+"isDead!");
		StartCoroutine (Respawn());
	}

	[ClientRpc]
	public void RpcTakeDamage(int _amount){
		currentHealth -= _amount;
		Debug.Log (transform.name + " now has " + currentHealth + " health");
		if (isDead)
			return;
		if(currentHealth<=0){
			Die();
		}
	}

	public void setDefault(){
		currentHealth = maxhealth;
		isDead = false;
		for(int i=0;i<disableOnDealth.Length;i++){
			disableOnDealth[i].enabled=wasEnabled[i];

		}
		Collider col=GetComponent<Collider>();
		if(col!=null){
			col.enabled = true;
		}
	}
	#endregion
}
