using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour {

    public bool Open;

    public float Modifier;

    void Start ()
    {
        Open = true;
	}
	
	void Update ()
    {
        if (Open)
        {
            transform.rotation = Quaternion.Euler(-90.0f, (90.0f * Modifier), 0.0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
        }
	}
}
