using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSpotlightEffect : MonoBehaviour
{
	// Editor properties.
	public Light refSpotlight;
	public Transform refTransSpotlight;
	public Transform refTransCone;

	[Space(10)]

	public float openInitialDelay;
	public float openDuration;

	// Run-time variables.
	private float savedLightIntensity;
	private Vector3 savedSpotlightSize;
	private float savedConeX;
	private bool isOpening = false;
	private float progress = 0f;

    private void Start()
    {
		// Save values.
		savedLightIntensity = refSpotlight.intensity;
		savedSpotlightSize = refTransSpotlight.transform.localScale;
		savedConeX = refTransCone.localScale.x;

		// Set initial values (off).
		refSpotlight.intensity = 0f;
		refTransSpotlight.localScale = Vector3.zero;
		refTransCone.localScale = new Vector3
		(
			0f, 
			refTransCone.localScale.y, 
			refTransCone.localScale.z
		);

		isOpening = false;

		// Start open spotlight routine, in a while.
		StartCoroutine(RoutineDelayedOpenSpotlight());
	}

	private void Update()
	{
		// Update cone angle to match camera always.
		refTransCone.transform.eulerAngles = new Vector3
		(
			DemoCameraController.main.currentCameraAngle,
			refTransCone.transform.eulerAngles.y,
			refTransCone.transform.eulerAngles.z
		);

		// Get input to toggle aperture.
		if (Input.GetKeyDown(KeyCode.Space))
		{
			isOpening = !isOpening;
		}

		// Update the aperture (open/close of the spotlight effect).
		UpdateSpotlightAperture();
	}

	private void UpdateSpotlightAperture()
	{
		// Update progress.
		progress = Mathf.Clamp01
		(
			progress + (Time.deltaTime * (isOpening ? 1f : -1f) * (1f / openDuration))
		);

		// Set spotlight aperature appropriately.
		float t = Mathf.Sin(progress * Mathf.PI * 0.5f);

		refSpotlight.intensity = Mathf.Lerp(0f, savedLightIntensity, t * t);

		refTransSpotlight.localScale = Vector3.Lerp
		(
			Vector3.zero,
			savedSpotlightSize,
			t
		);

		refTransCone.localScale = new Vector3
		(
			Mathf.Lerp(0f, savedConeX, t),
			refTransCone.localScale.y,
			refTransCone.localScale.z
		);
	}

	private IEnumerator RoutineDelayedOpenSpotlight()
	{
		// Wait initial delay.
		yield return new WaitForSeconds(openInitialDelay);

		isOpening = true;

		//// Lerp expand the spotlight (ease out).
		//float startTime = Time.time;
		//float t = 0;
		//while (Time.time - openDuration < startTime)
		//{
		//	// Ease out.
		//	t = (Time.time - startTime) / openDuration;
		//	t = Mathf.Sin(t * Mathf.PI * 0.5f);	

		//	refSpotlight.intensity = Mathf.Lerp(0f, savedLightIntensity, t*t);

		//	refTransSpotlight.localScale = Vector3.Lerp
		//	(
		//		Vector3.zero,
		//		savedSpotlightSize,
		//		t
		//	);

		//	refTransCone.localScale = new Vector3
		//	(
		//		Mathf.Lerp(0f, savedConeX, t),
		//		refTransCone.localScale.y,
		//		refTransCone.localScale.z
		//	);

		//	yield return null;
		//}

		//// Finalize.
		//refSpotlight.intensity = savedLightIntensity;
	}
}
