using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineofSightController : MonoBehaviour {

    public List<GameObject> Lights;

    GameObject User;

    public Vector3[] ActiveLights = new Vector3[4];

    public float CurrentDistance;
    public  float[] ClosestDistances = new float[4];
    public bool Activate;

    void Start ()
    {
        User = GameObject.Find("Camera");
        
        foreach (Transform Child in transform)
        {
            Lights.Add(Child.gameObject);

            CurrentDistance = Vector3.Distance(Child.transform.position, User.transform.position);

            if (ClosestDistances[0] == 0.0f)
            {
                ClosestDistances[0] = CurrentDistance;
                ActiveLights[0] = Child.transform.position;
            }
            else if (ClosestDistances[1] == 0.0f)
            {
                ClosestDistances[1] = CurrentDistance;
                ActiveLights[1] = Child.transform.position;
            }
            else if (ClosestDistances[2] == 0.0f)
            {
                ClosestDistances[2] = CurrentDistance;
                ActiveLights[2] = Child.transform.position;
            }
            else if (ClosestDistances[3] == 0.0f)
            {
                ClosestDistances[3] = CurrentDistance;
                ActiveLights[3] = Child.transform.position;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (ClosestDistances[i] > CurrentDistance)
                    {
                        ClosestDistances[i] = CurrentDistance;
                        ActiveLights[i] = Child.transform.position;
                        break;
                    }
                }
            }
        }

	}

	void Update ()
    {
        foreach (GameObject Child in Lights)
        {
            CurrentDistance = Vector3.Distance(Child.transform.position, User.transform.position);

            for (int i = 0; i < 4; i++)
            {
                if (ClosestDistances[i] > CurrentDistance)
                {
                    ClosestDistances[i] = CurrentDistance;
                    ActiveLights[i] = Child.transform.position;
                    break;
                }
            }
        }

        foreach (GameObject Child in Lights)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Child.transform.position == ActiveLights[i])
                {
                    Child.gameObject.SetActive(true);
                    break;
                }
                else
                {
                    Child.gameObject.SetActive(false);
                }
            }
        }
    }
}
