using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControl : MonoBehaviour {

    public GameObject GO_Blade_1;
    public GameObject GO_Blade_2;
    public GameObject GO_Blade_3;
    public GameObject GO_Blade_4;

    GameObject GO_Node;

    public float f_Blade_Speed = 10000.0f;

    public Vector3 V3_Next_Pos;

    FlightPathNodes FPN;

    public float speed = 0.1f;
    private float startTime;
    private float journeyLength;

    void Start ()
    {
        GO_Blade_1 = this.transform.GetChild(0).gameObject;
        GO_Blade_2 = this.transform.GetChild(1).gameObject;
        GO_Blade_3 = this.transform.GetChild(2).gameObject;
        GO_Blade_4 = this.transform.GetChild(3).gameObject;

        FPN = GameObject.Find("Flight Path Start/End").GetComponent<FlightPathNodes>();
        GO_Node = FPN.GO_FP_Node;
        V3_Next_Pos = FPN.V3_Node_Pos;
        
        startTime = Time.time;
        journeyLength = Vector3.Distance(transform.position, V3_Next_Pos);
    }
	
	void Update ()
    {
        GO_Blade_1.transform.Rotate(Vector3.up, f_Blade_Speed * Time.deltaTime, Space.World);
        GO_Blade_2.transform.Rotate(Vector3.up, f_Blade_Speed * Time.deltaTime, Space.World);
        GO_Blade_3.transform.Rotate(Vector3.up, f_Blade_Speed * Time.deltaTime, Space.World);
        GO_Blade_4.transform.Rotate(Vector3.up, f_Blade_Speed * Time.deltaTime, Space.World);


        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, V3_Next_Pos, fracJourney);

        transform.LookAt(GO_Node.transform);
    }

    void OnTriggerEnter(Collider Hit)
    {
        Debug.Log("Hit " + Hit.gameObject.name);
        if (Hit.gameObject.name != "Flight Path Start/End")
        {
            FPN = Hit.gameObject.GetComponent<FlightPathNodes>();
            GO_Node = FPN.GO_FP_Node;
            V3_Next_Pos = FPN.V3_Node_Pos;
            startTime = Time.time;
            journeyLength = Vector3.Distance(transform.position, V3_Next_Pos);
        }
    }
}
