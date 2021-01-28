using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoControlsTest : MonoBehaviour
{
    public RawImage refControlsTest;

	[Space(10)]
	public float initialDelay;
	public float blinkDuration;
	public float blinkRate;

	private float initialDelayTimer;
	private float durationTimer;
	private float blinkTimer;

	private bool isBlinkedOff = true;

	private void Start()
	{
		refControlsTest.enabled = false;
	}

	private void Update()
	{
		if (initialDelayTimer < initialDelay)
		{
			initialDelayTimer += Time.deltaTime;
			return;
		}

		if (durationTimer < blinkDuration)
		{
			durationTimer += Time.deltaTime;

			if (blinkTimer < blinkRate)
			{
				blinkTimer += Time.deltaTime;
			}
			else
			{
				blinkTimer = 0f;
				isBlinkedOff = !isBlinkedOff;
				refControlsTest.enabled = isBlinkedOff;
			}
		}
		else
		{
			refControlsTest.enabled = false;
		}
	}

}
