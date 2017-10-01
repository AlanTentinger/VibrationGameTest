using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNumberFaceCamera : MonoBehaviour {

	public GameObject cameraToFace;

	// Use this for initialization
	void Start () {
		cameraToFace = GameObject.FindGameObjectWithTag ("MainCamera");
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 directionToCamera = cameraToFace.transform.position - transform.position;

		directionToCamera.x = 0.0f;
		directionToCamera.z = 0.0f;
		transform.LookAt( cameraToFace.transform.position - directionToCamera ); 
		transform.rotation = (cameraToFace.transform.rotation);
		//transform.Rotate(0,180,0);
	}
}
