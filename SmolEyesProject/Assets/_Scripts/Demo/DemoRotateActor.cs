using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoRotateActor : MonoBehaviour
{
	//public Vector3 rotation;

	void Update()
    {
		this.transform.Rotate
		(
			(Vector3.up * DemoCameraController.currentRotationSpeed) * 
			Time.deltaTime
		);
	}
}
