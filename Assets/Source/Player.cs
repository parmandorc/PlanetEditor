using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For now, this only manages miscellaneous player input
public class Player : MonoBehaviour
{	
	[SerializeField]
	public GameObject canvas;

	void Update ()
	{
		if (Input.GetButtonDown("ToggleUI"))
		{
			canvas.SetActive(!canvas.activeSelf);
		}
	}
}
