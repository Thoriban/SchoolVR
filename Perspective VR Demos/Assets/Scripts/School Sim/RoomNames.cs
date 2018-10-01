using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNames : MonoBehaviour {

    private GameObject GO_Camera;

	void Start ()
    {
        GO_Camera = GameObject.Find("Camera (eye)");
    }
	
	void Update ()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - GO_Camera.transform.position);
    }
}
