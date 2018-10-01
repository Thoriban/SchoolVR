using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorControl : MonoBehaviour
{

	private float y_pos;

	void Start()
	{
		y_pos = transform.position.y;
	}

	void Update()
	{
		if (transform.position.y < y_pos)
		{
			transform.position = new Vector3(transform.position.x, y_pos, transform.position.z);
		}
	}
}