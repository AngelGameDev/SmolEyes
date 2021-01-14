using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCameraController : MonoBehaviour
{
	public static DemoCameraController main;

	// Editor properties.
	public Transform viewTarget;

	[Space(10)]

    public float maxZoomIn;
	public float maxZoomOut;
	public float maxRot;

	[Space(10)]

	public float zoomSpeed;
	public float rotationSpeed;

	// Run-time fields.
	private float currentZoomProgress = 0.5f;
	private float currentRotProgress = 0.5f;

	public float currentRotationSpeed = 0f;
	public float currentCameraAngle;
	private float targetZoom;
	private float targetRotProgress;
	private float targetZoomProgress;

	private void Awake()
	{
		main = this;
	}

	private void Start()
	{
		currentRotationSpeed = 0f;
		currentCameraAngle = this.transform.eulerAngles.x;
	}

	private void Update()
    {
		GetInput();
		UpdateProgress();
		ApplyUpdate();

		currentCameraAngle = this.transform.eulerAngles.x;
	}

	private void GetInput()
	{
		// Rotation.
		targetRotProgress = 0.5f;
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			targetRotProgress -= 0.5f;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			targetRotProgress += 0.5f;
		}

		// Zoom.
		targetZoomProgress = 0.5f;
		if (Input.GetKey(KeyCode.UpArrow))
		{
			targetZoomProgress -= 0.5f;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			targetZoomProgress += 0.5f;
		}
	}

	private void UpdateProgress()
	{
		// Rotation.
		currentRotProgress = Mathf.MoveTowards
		(
			currentRotProgress, 
			targetRotProgress,
			rotationSpeed * Time.deltaTime
		);

		// Zoom.
		currentZoomProgress = Mathf.MoveTowards
		(
			currentZoomProgress,
			targetZoomProgress,
			zoomSpeed * Time.deltaTime
		);
	}

	private void ApplyUpdate()
	{
		// Apply zoom.
		this.transform.localPosition = new Vector3
		(
			this.transform.localPosition.x,
			this.transform.localPosition.y,
			Mathf.Lerp(maxZoomIn, maxZoomOut, currentZoomProgress)
		);

		this.transform.LookAt(viewTarget.position);

		// Apply rotation speed (used elsewhere).
		currentRotationSpeed = Mathf.Lerp
		(
			-1 * maxRot, 
			maxRot, 
			currentRotProgress
		);
	}
}
