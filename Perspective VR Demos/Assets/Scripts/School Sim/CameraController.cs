using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float speedF = 35.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float wait = 0.0f;
    private float cooldown;

    private GameObject GO_Facing;

    private GameObject GO_1st_Floor_Normal;
    private GameObject GO_1st_Floor_Transparent;
    private GameObject GO_1st_Floor_Windows;

    private GameObject GO_2nd_Floor_Normal;
    private GameObject GO_2nd_Floor_Transparent;
    private GameObject GO_2nd_Floor_Windows;

    private GameObject GO_Roof;

    private bool b_Flight_Mode;
    private bool b_Transparent_1st;
    private bool b_Transparent_2nd;

    private int i_Active_Floor;

    private Scene Current;

    private Rigidbody RB_Camera;

    private void Start()
    {
        Current = SceneManager.GetActiveScene();

        i_Active_Floor = 3;

        GO_Facing = GameObject.Find("Facing");

        if (Current.name == "School Camera")
        {
            GO_1st_Floor_Normal = GameObject.Find("1st Floor Normal");
            GO_1st_Floor_Transparent = GameObject.Find("1st Floor Transparent Exterior");
            GO_1st_Floor_Windows = GameObject.Find("1st Floor Windows");

            GO_2nd_Floor_Normal = GameObject.Find("2nd Floor Normal");
            GO_2nd_Floor_Transparent = GameObject.Find("2nd Floor Transparent Exterior");
            GO_2nd_Floor_Windows = GameObject.Find("2nd Floor Windows");

            GO_Roof = GameObject.Find("School Roof");

            GO_1st_Floor_Normal.SetActive(true);
            GO_1st_Floor_Transparent.SetActive(false);
            GO_1st_Floor_Windows.SetActive(true);

            GO_2nd_Floor_Normal.SetActive(true);
            GO_2nd_Floor_Transparent.SetActive(false);
            GO_2nd_Floor_Windows.SetActive(true);

            GO_Roof.SetActive(true);

            cooldown = 0.5f;

            b_Flight_Mode = false;
            b_Transparent_1st = false;
        }
        else if (Current.name == "Virtual Conference")
        {
            RB_Camera = this.gameObject.GetComponent<Rigidbody>();
        }
    }
    
    void Update()
    {
        Timing();

        if (Current.name == "School Camera")
        {
            Alter_Transparency();
            Change_Active_Floor();
            Movement_School();
        }
        else if (Current.name == "Virtual Conference")
        {
            Movement_Conference();
        }

    }

    void Alter_Transparency()
    {
        if (i_Active_Floor >= 2)
        {
            if (Input.GetKey(KeyCode.Keypad1) && wait == 0.0f)
            {
                First_Floor_Trans();
            }

            if (Input.GetKey(KeyCode.Keypad2) && wait == 0.0f)
            {
                Second_Floor_Trans();
            }
        }
        else if (i_Active_Floor == 1)
        {
            if (Input.GetKey(KeyCode.Keypad1) && wait == 0.0f)
            {
                First_Floor_Trans();
            }
        }
    }

    void Movement_School()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (Input.GetKey(KeyCode.Space) && wait == 0.0f)
        {
            if (b_Flight_Mode)
            {
                b_Flight_Mode = false;
                wait = cooldown;
            }
            else
            {
                b_Flight_Mode = true;
                wait = cooldown;
            }
        }

        if (b_Flight_Mode)
        {
            if (Input.GetKey(KeyCode.W))
            {
                this.transform.position += transform.forward * Time.deltaTime * speedF;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                this.transform.position += GO_Facing.transform.forward * Time.deltaTime * speedF;
            }
        }
    }

    void Movement_Conference()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        b_Flight_Mode = false;

        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += GO_Facing.transform.forward * Time.deltaTime * speedF;
            b_Flight_Mode = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.transform.position += GO_Facing.transform.forward * Time.deltaTime * speedF * -1.0f;
            b_Flight_Mode = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += GO_Facing.transform.right * Time.deltaTime * speedF;
            b_Flight_Mode = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += GO_Facing.transform.right * Time.deltaTime * speedF * -1.0f;
            b_Flight_Mode = true;
        }

        if(!b_Flight_Mode)
        {
            RB_Camera.velocity = new Vector3(0, 0, 0);
        }
    }

    void Change_Active_Floor()
    {
        if (Input.GetKey(KeyCode.UpArrow) && wait == 0.0f && i_Active_Floor < 3)
        {
            i_Active_Floor += 1;
            wait = cooldown;

            if (i_Active_Floor >= 1)
            {
                GO_1st_Floor_Normal.SetActive(true);
                GO_1st_Floor_Windows.SetActive(true);
            }

            if (i_Active_Floor >= 2)
            {
                GO_2nd_Floor_Normal.SetActive(true);
                GO_2nd_Floor_Windows.SetActive(true);
            }

            if (i_Active_Floor == 3)
            {
                GO_Roof.SetActive(true);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) && wait == 0.0f && i_Active_Floor > 0)
        {
            i_Active_Floor -= 1;
            wait = cooldown;

            if (i_Active_Floor <= 2)
            {
                GO_Roof.SetActive(false);
            }

            if (i_Active_Floor <= 1)
            {
                GO_2nd_Floor_Normal.SetActive(false);
                GO_2nd_Floor_Transparent.SetActive(false);
                GO_2nd_Floor_Windows.SetActive(false);
            }

            if (i_Active_Floor == 0)
            {
                GO_1st_Floor_Normal.SetActive(false);
                GO_1st_Floor_Transparent.SetActive(false);
                GO_1st_Floor_Windows.SetActive(false);
            }
        }
    }

    void First_Floor_Trans()
    {
        if (b_Transparent_1st && i_Active_Floor >= 1)
        {
            GO_1st_Floor_Normal.SetActive(true);
            GO_1st_Floor_Transparent.SetActive(false);

            b_Transparent_1st = false;
            wait = cooldown;
        }
        else
        {
            GO_1st_Floor_Normal.SetActive(false);
            GO_1st_Floor_Transparent.SetActive(true);
            b_Transparent_1st = true;
            wait = cooldown;
        }
    }

    void Second_Floor_Trans()
    {
        if (b_Transparent_2nd && i_Active_Floor >= 2)
        {
            GO_2nd_Floor_Normal.SetActive(true);
            GO_2nd_Floor_Transparent.SetActive(false);

            b_Transparent_2nd = false;
            wait = cooldown;
        }
        else
        {
            GO_2nd_Floor_Normal.SetActive(false);
            GO_2nd_Floor_Transparent.SetActive(true);
            b_Transparent_2nd = true;
            wait = cooldown;
        }
    }

    void Timing()
    {
        if (wait > 0.0f)
        {
            wait -= Time.deltaTime;
        }
        if (wait < 0.0f)
        {
            wait = 0.0f;
        }
    }
}
