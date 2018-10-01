using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControl : MonoBehaviour {

    GameObject GO_World, GO_Player, GO_Helmet;

    Renderer Rend;

    Vector3 V3_World_Pos = new Vector3(0, 0, 0);

    Vector3 V3_First_Island, V3_Second_Island;

    float x, y, z, delta_y, end_y;

    float f_wait, f_cooldown;

    float f_Scaled;

    public bool b_Raised, b_Helmet;

    int i_Island;

    private void Start()
    {



        GO_World = GameObject.Find("Island Land Mass");
        GO_Player = GameObject.Find("[CameraRig]");
        GO_Helmet = GameObject.Find("Helmet");

        Rend = this.gameObject.GetComponent<Renderer>();
        Rend.enabled = true;

        V3_World_Pos = GO_World.transform.position;
        V3_First_Island = GO_Player.transform.position;  //(338.0f, 5.0f, 221.0f)
        V3_Second_Island = new Vector3(374.0f, 5.0f, 1244.0f);

        x = V3_World_Pos.x;
        y = V3_World_Pos.y;
        z = V3_World_Pos.z;

        delta_y = y - 60;

        b_Raised = false;
        b_Helmet = false;

        f_wait = 0.0f;
        f_cooldown = 1.0f;
        f_Scaled = 2.0f;
        i_Island = 1;
    }

    private void Update()
    {
        Timing();

        Movement();
        if (b_Helmet == false)
        {
            GO_Helmet.SetActive(false);
            Rend.enabled = true;
        }
        else
        {
            GO_Helmet.SetActive(true);
            Rend.enabled = false;
            //Movement();
        }

        if (f_wait == 0.0f)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                Lift();
            }
            else if (Input.GetKey(KeyCode.KeypadPlus))
            {
                Grow();
            }
            else if (Input.GetKey(KeyCode.KeypadMinus))
            {
                Shrink();
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                ChangeIsland();
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                Helmet();

            }
        }
        
    }

    public void Helmet()
    {
        if (b_Helmet == false)
        {
            b_Helmet = true;
        }
        else
        {
            b_Helmet = false;
        }

        f_wait = f_cooldown;
    }

    public void Movement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GO_Player.transform.position += GO_Helmet.transform.forward * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            GO_Player.transform.position -= GO_Helmet.transform.forward * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            GO_Player.transform.position += GO_Helmet.transform.right * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            GO_Player.transform.position -= GO_Helmet.transform.right * Time.deltaTime;
        }
    }

    public void ChangeIsland()
    {
        if (i_Island == 1)
        {
            GO_Player.transform.position = V3_Second_Island;
            i_Island = 2;
        }
        else if (i_Island == 2)
        {
            GO_Player.transform.position = V3_First_Island;
            i_Island = 1;
        }

        f_wait = f_cooldown;
    }

    public void Lift()
    {
        if (b_Raised == false)
        {
            GO_World.transform.position = new Vector3(x, delta_y, z);
            b_Raised = true;
        }
        else
        {
            GO_World.transform.position = new Vector3(x, y, z);
            b_Raised = false;
        }
        f_wait = f_cooldown;
    }

    public void Grow()
    {
        if (f_Scaled == 2.0f)
        {
            f_Scaled = 10.0f;
        }
        else if (f_Scaled == 0.5f)
        {
            f_Scaled = 2.0f;
        }

        GO_Player.transform.localScale = new Vector3(f_Scaled, f_Scaled, f_Scaled);
        f_wait = f_cooldown;
    }

    public void Shrink()
    {
        if (f_Scaled == 10.0f)
        {
            f_Scaled = 2.0f;
        }
        else if (f_Scaled == 2.0f)
        {
            f_Scaled = 0.5f;
        }

        GO_Player.transform.localScale = new Vector3(f_Scaled, f_Scaled, f_Scaled);
        f_wait = f_cooldown;
    }

    private void Timing()
    {
        if (f_wait > 0.0f)
        {
            f_wait -= Time.deltaTime;
        }

        if (f_wait < 0.0f)
        {
            f_wait = 0.0f;
        }
    }
}
