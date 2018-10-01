using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightPathNodes : MonoBehaviour {

    public GameObject GO_FP_Node;
    public Vector3 V3_Node_Pos;

	void Awake ()
    {
        V3_Node_Pos = GO_FP_Node.transform.position;
	}
	
	void Update () {
		
	}
}
