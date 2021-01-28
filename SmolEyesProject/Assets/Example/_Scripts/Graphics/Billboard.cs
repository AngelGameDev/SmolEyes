using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	public Camera target;

	private void Update()
	{
		transform.LookAt(target.transform.position);
	}
}
