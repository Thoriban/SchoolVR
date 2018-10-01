using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRControl : MonoBehaviour {

	private Valve.VR.EVRButtonId TriggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private Valve.VR.EVRButtonId ApplicationMenuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
	private Valve.VR.EVRButtonId GripButton = Valve.VR.EVRButtonId.k_EButton_Grip;

	private SteamVR_Controller.Device device;

	public GameObject GO_Helmet;
	public GameObject GO_Player;
	public GameObject GO_Facing;
	public GameObject GO_Line_of_Sight;
	public GameObject GO_Names;

	private SteamVR_TrackedObject TrackedObject;
	private SteamVR_TrackedController Controller;
	private ElevatorControl EC;
	private Facing FC;
	
	public string s_Flow_Type;

	public float f_Speed;
	private float f_delta;
	private float wait;
	private float cooldown;

	public bool b_Move_World;
	public bool b_Line_of_Sight;
	public bool b_Room_Names;

	void Start ()
	{
		
		GO_Player = GameObject.Find("[CameraRig]");
		GO_Helmet = GameObject.Find("SWAT Visor");
		GO_Facing = GameObject.Find("Facing");
		GO_Line_of_Sight = GameObject.Find("Line of Sights");
		GO_Names = GameObject.Find("Room Names");

		FC = GO_Facing.GetComponent<Facing>();
		TrackedObject = GetComponent<SteamVR_TrackedObject>();
		Controller = GetComponent<SteamVR_TrackedController>();

		f_delta = 0.5f;    
		
		f_Speed = 10.0f;

		b_Move_World = true;
		b_Line_of_Sight = false;
		b_Room_Names = false;

		GO_Names.SetActive(false);
		GO_Line_of_Sight.SetActive(false);

		wait = 0.0f;
		cooldown = 1.0f;
	}

	void Update ()
	{
		device = SteamVR_Controller.Input((int)TrackedObject.index);
		Timing();

		if (device.GetPressDown(TriggerButton) && wait == 0.0f)
		{
			LineOfSight();
		}

		if (device.GetPressDown(ApplicationMenuButton) && wait == 0.0f)
		{
			Flight_Mode();
		}

		if (device.GetPressDown(GripButton) && wait == 0.0f)
		{
			Names();
		}

		if (b_Move_World)
		{

			if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
			{
				Vector2 touchpad = (device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0));

				if (touchpad.y > 0.7f)
				{
					GO_Player.transform.position += GO_Facing.transform.forward * Time.deltaTime * f_Speed;
				}

				if (touchpad.x > 0.7f)
				{
					GO_Player.transform.position += GO_Facing.transform.right * Time.deltaTime * f_Speed;
				}
				else if (touchpad.x < -0.7f)
				{
					GO_Player.transform.position -= GO_Facing.transform.right * Time.deltaTime * f_Speed;
				}
			}
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

	void LineOfSight()
	{
		if (b_Line_of_Sight)
		{
			b_Line_of_Sight = false;
			GO_Line_of_Sight.SetActive(false);
			wait = cooldown;
		}
		else
		{
			b_Line_of_Sight = true;
			GO_Line_of_Sight.SetActive(true);
			wait = cooldown;
		}
	}

	void Flight_Mode()
	{
		if (FC.b_Flight_Mode)
		{
			FC.b_Flight_Mode = false;
		}
		else
		{
			FC.b_Flight_Mode = true;
		}
	}

	void Names()
	{
		if (b_Room_Names)
		{
			GO_Names.SetActive(false);
			b_Room_Names = false;
			wait = cooldown;
		}
		else
		{
			GO_Names.SetActive(true);
			b_Room_Names = true;
			wait = cooldown;
		}
	}

	void Toggle()
	{
		if (b_Move_World)
		{
			b_Move_World = false;
			GO_Helmet.SetActive(false);
		}
		else
		{
			b_Move_World = true;
			GO_Helmet.SetActive(true);
		}
	}
}
