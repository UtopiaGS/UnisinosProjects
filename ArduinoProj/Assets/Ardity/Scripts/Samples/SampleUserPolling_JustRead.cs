/**
 * SerialCommUnity (Serial Communication for Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */
/*
move = 0x0001;
left mouse down = 0x0002;
left mouse up = 0x0004;
right mouse down = 0x0008;
right mouse up = 0x0010;
middle mouse down = 0x0020;
middle mouse up = 0x0040;
private const int absolute = 0x8000;  

more info about mouse_event https://msdn.microsoft.com/en-us/library/windows/desktop/ms646260(v=vs.85).aspx
*/

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Globalization;

/**
 * Sample for reading using polling by yourself. In case you are fond of that.
 */
public class SampleUserPolling_JustRead : MonoBehaviour
{
    public SerialController serialController;

	public string[] messages;

	public Quaternion initialRot;
	public Transform obj;
	public Vector3 oldRot;
	public Vector3 newRot;

	public float differenceX;
	public float differenceY;

	public float sensivityX;
	public float sensivityY;

	public bool clicking1;
	public bool clicking2;

	public bool ShootInput;

	public ShootingBehaviour Shooting;

	public float offOffset = 23;

	public float InputShot = 0;
	/*
	[DllImport("user32.dll")]
	static extern void mouse_event (int flag, int x, int y, int data, int extraInfo);

	public void Move(int x, int y){
		mouse_event (0x0001, x, y, 0, 0);
	}

	public void MouseDown(){
		mouse_event (0x0002, 0, 0, 0, 0);
	}

	public void MouseUp(){
		mouse_event (0x0004, 0, 0, 0, 0);
	}

	public void ButtonDown(){
		//Second button code goes here
	}

	public void ButtonUp(){
		//Second button code goes here
	}
	*/
    // Initialization
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
		initialRot = obj.rotation;
	}

    // Executed each frame
    void Update()
    {
        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Connection attempt failed or disconnection detected");

		messages = message.Split (" " [0]);
		Debug.Log("Message: "+ message);

		if (message == "__Connected__" || message == "__Disconnected__") { 
			Debug.Log("Trying to connect " + message);
			return; 
		}

		if (messages.Length > 0)
		{
			/*			
			if (messages.Length>=3 && messages[3] == "0" && !clicking1)
			{
				clicking1 = true;
				//MouseDown ();
			}
			if (messages.Length >= 3 && messages[3] == "1" && clicking1)
			{
				clicking1 = false;
				//MouseUp ();
			}

			if (messages.Length >=4 && messages[4] == "0" && !clicking2)
			{
				clicking2 = true;
				//ButtonDown ();
			}
			if (messages.Length >= 4 && messages[4] == "1" && clicking2)
			{
				clicking2 = false;
				//ButtonUp ();
			}*/

			oldRot = obj.eulerAngles;
			obj.eulerAngles = new Vector3(-float.Parse(messages[2], CultureInfo.InvariantCulture.NumberFormat),
				float.Parse(messages[0], CultureInfo.InvariantCulture.NumberFormat),
				float.Parse(messages[1], CultureInfo.InvariantCulture.NumberFormat)) + initialRot.eulerAngles ;

			InputShot = float.Parse(messages[5], CultureInfo.InvariantCulture.NumberFormat);

			if (messages.Length >= 5 && messages[5] == "0")
			{
				ShootInput = false;
			}

			if (messages.Length >= 5 && messages[5] == "1" && !ShootInput)
			{
				Debug.Log("ATIROUUUUUUUUUUUUUUUUUUUUUUUUUU " + InputShot);
				Shooting.Shooting();
				ShootInput = true;
			}
			
		}

		message = string.Empty;
	}
}
