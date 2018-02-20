using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(playermotor))]

public class playercontroller : MonoBehaviour {
	[SerializeField]
	private float speed=5f;
	[SerializeField]
	private float looksensitivity=3f;
	[SerializeField]
	private float thrusterForce=1000f;

	private playermotor motor;
	void Start(){
		motor = GetComponent<playermotor> ();
	}
	void Update(){
		//calculate movement velocity a 3d vector
		float _xMov=Input.GetAxisRaw("Horizontal");
		float _zMov = Input.GetAxisRaw ("Vertical");
		Vector3 _movHorizontal = transform.right * _xMov;
		Vector3 _movVertical = transform.forward * _zMov;
		Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;
		motor.Move (_velocity);
		//calculate rotation
		float _yRot=Input.GetAxisRaw("Mouse X");
		Vector3 _rotation = new Vector3 (0f, _yRot, 0f) * looksensitivity;
		//apply Rotation
		motor.Rotation(_rotation);

		//calculate rotation
		float _xRot=Input.GetAxisRaw("Mouse Y");
		Vector3 _rotation_x = new Vector3 (_xRot, 0f, 0f) * looksensitivity;
		//applying thruster

		Vector3 _thrusterForce = Vector3.zero;
		if(Input.GetButton("Jump")){
			
			_thrusterForce=Vector3.up * thrusterForce;
		}
		motor.ApplyThruster(_thrusterForce);

		//apply camera Rotation
		motor.Camera_Rotation(_rotation_x);
	}
}
