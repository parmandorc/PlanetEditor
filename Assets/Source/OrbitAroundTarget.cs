using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAroundTarget : MonoBehaviour 
{
	[SerializeField]
	private GameObject target;

	[SerializeField]
	private float autoSpeed;

	[SerializeField]
	private float inputSpeed;

	[SerializeField]
	private float stopTimeAfterNoInput;

	[SerializeField]
	private float transitionTimeAfterNoInput;

	private float timeSinceLastInput;

	void Update () 
	{
		float delta = autoSpeed;
		float input = Input.GetAxis("Horizontal");

		if (input != 0.0f) 
		{
			delta = input * inputSpeed;

			timeSinceLastInput = stopTimeAfterNoInput + transitionTimeAfterNoInput;
		} 
		else if (timeSinceLastInput > 0.0f) 
		{
			timeSinceLastInput -= Time.deltaTime;

			if (timeSinceLastInput > transitionTimeAfterNoInput)
			{
				delta = 0.0f;
			}
			else
			{
				delta = Mathf.Lerp(0.0f, autoSpeed, Mathf.InverseLerp(transitionTimeAfterNoInput, 0.0f, timeSinceLastInput));
			}
		}

		transform.RotateAround(target.transform.position, Vector3.up, -Time.deltaTime * delta);
		transform.LookAt(target.transform);
	}
}
