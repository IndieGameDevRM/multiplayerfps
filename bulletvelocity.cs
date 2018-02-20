using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class bulletvelocity : MonoBehaviour {
	[SerializeField]
	private float bulletspeed = 10;
	[SerializeField]
	private float bulletprojectile;
	void Start(){
		GetComponent<Rigidbody> ().AddForce (transform.forward*bulletspeed);
		GetComponent<Rigidbody> ().AddForce (transform.up*bulletprojectile);
	
	}
	void OnCollisionEnter(Collision col){
		Destroy (this.gameObject);
	}

}
