using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAroundTarget : MonoBehaviour 
{
	[SerializeField]
	private GameObject target;

	[SerializeField]
	private float speed;

	void Update () 
	{
		transform.RotateAround(target.transform.position, Vector3.up, Time.deltaTime * speed);
		transform.LookAt(target.transform);
	}
}
