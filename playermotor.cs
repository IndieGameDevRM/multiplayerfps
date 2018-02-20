using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class playermotor : MonoBehaviour {
	[SerializeField]
	private Camera cam;
	private Vector3 thrusterForce=Vector3.zero;
	private Rigidbody rb;
	[SerializeField]
	private float CameraRotationLimit=75;
	private float currentCameraRotationX=0f;
	private Vector3 velocity = Vector3.zero;
	private Vector3 Player_rotation = Vector3.zero;
	private Vector3 CameraRotation=Vector3.zero;
	void Start () {
		rb = GetComponent<Rigidbody> ();

	}
	
	//Get a movement
	public void Move(Vector3 _velocity){
		velocity = _velocity;
	}
	//Get a Rotation
	public void Rotation(Vector3 _rotation){
		Player_rotation = _rotation;
	}
	//Get a Camera Rotation
	public void Camera_Rotation(Vector3 cam_rotation){
		currentCameraRotationX = cam_rotation.x;
	}
	//Get thruster force
	public void ApplyThruster(Vector3 _thrusterFoce){
		thrusterForce = _thrusterFoce;
	}
	void FixedUpdate () {
		PerformMovement ();
		PerformRotation ();
	}
	//perform movement based on velocity variable

	void PerformMovement(){
		if (velocity != Vector3.zero) {
			rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);
		} else if (thrusterForce != Vector3.zero) {
			rb.AddForce (thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
		}
	}
	void PerformRotation (){
		rb.MoveRotation (rb.rotation * Quaternion.Euler (Player_rotation));
		//Mathf.Clamp (currentCameraRotationX, CameraRotationLimit, -CameraRotationLimit);
		if (currentCameraRotationX > CameraRotationLimit) {
			currentCameraRotationX = CameraRotationLimit;
		}else if(currentCameraRotationX<-CameraRotationLimit){
			currentCameraRotationX = -CameraRotationLimit;
		}
		//cam.transform.localEulerAngles = new Vector3 (-CameraRotationLimit,0f,0f);
		cam.transform.Rotate (-currentCameraRotationX, 0f, 0f);

	}
}
