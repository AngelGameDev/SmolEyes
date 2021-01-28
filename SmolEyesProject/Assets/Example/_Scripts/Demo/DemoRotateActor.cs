using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoRotateActor : MonoBehaviour
{
	//public Vector3 rotation;
	public float maxAnimSpeed;

	private Animator refAnimator;

	private void Start()
	{
		refAnimator = GetComponent<Animator>();
	}

	private void Update()
    {
		refAnimator.speed = Mathf.Lerp
		(
			0f,
			maxAnimSpeed, 
			Mathf.Abs(DemoCameraController.main.currentRotationSpeed)
				/ DemoCameraController.main.maxRot
		);

		this.transform.Rotate
		(
			Vector3.up  
			* DemoCameraController.main.currentRotationSpeed
			* Time.deltaTime
		);
	}
}
