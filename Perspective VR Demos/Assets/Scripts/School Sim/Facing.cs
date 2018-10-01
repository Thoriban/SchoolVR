using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Facing : MonoBehaviour
{

    public GameObject GO_Camera;
    public GameObject GO_Flight_Icon;

    private CameraController CC;

    private Quaternion Q_Facing;

    private Vector3 V_Facing;

    private Scene Current;

    private float yaw;

    public bool b_Flight_Mode;

    void Start()
    {
        Current = SceneManager.GetActiveScene();
        GO_Flight_Icon = GameObject.Find("Flight Mode Icon");

        if (Current.name == "School Vr")
        {
            GO_Camera = GameObject.Find("Camera (eye)");
        }
        else if (Current.name == "Test World")
        {
            GO_Camera = GameObject.Find("Camera");
            CC = GO_Camera.GetComponent<CameraController>();
        }
        else if (Current.name == "School Camera")
        {
            GO_Camera = GameObject.Find("Camera");
            CC = GO_Camera.GetComponent<CameraController>();
        }
        else if (Current.name == "Virtual Conference")
        {
            GO_Camera = GameObject.Find("Camera");
            CC = GO_Camera.GetComponent<CameraController>();
        }

    }

    void Update()
    {
        transform.position = GO_Camera.transform.position;

        if (Current.name == "School Vr")
        {
            if (b_Flight_Mode)
            {
                transform.rotation = GO_Camera.transform.rotation;
                GO_Flight_Icon.SetActive(true);
            }
            else
            {
                Q_Facing = GO_Camera.transform.rotation;
                V_Facing = Q_Facing.eulerAngles;
                V_Facing = new Vector3(0.0f, V_Facing.y, 0.0f);
                Q_Facing.eulerAngles = V_Facing;
                GO_Flight_Icon.SetActive(false);

                transform.rotation = Q_Facing;
            }
        }
        else if (Current.name == "Test World")
        {
            yaw += CC.speedH * Input.GetAxis("Mouse X");

            transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        }
        else if (Current.name == "School Camera"|| Current.name == "Virtual Conference")
        {
            yaw += CC.speedH * Input.GetAxis("Mouse X");

            transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        }

    }
}
