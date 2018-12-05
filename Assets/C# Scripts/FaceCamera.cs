using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

    [SerializeField] Vector3 healthRotation;

    [SerializeField] Camera mainCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(mainCamera.transform);
        transform.rotation = Quaternion.Euler(healthRotation);
		
	}
}
