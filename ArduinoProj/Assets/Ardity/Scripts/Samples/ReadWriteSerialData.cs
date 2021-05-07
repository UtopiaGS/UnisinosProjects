/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

/**
 * Sample for reading using polling by yourself, and writing too.
 */
public class ReadWriteSerialData : MonoBehaviour
{
    public SerialController serialController;

    public Transform obj;
    public Vector3 oldRot;
    public Vector3 newRot;

    public float differenceX;
    public float differenceY;

    [DllImport("user32.dll")]
    static extern void move_event(int flag, int x, int y, int data, int extraInfo);

    public Vector3 Accelerometer;
    public Vector3 Gyroscope;

    public string Message;
    public string[] MessageSplit;

    public GameObject ObjectToMove;

    public void Move(int x, int y) {
        move_event(0x0001, x, y, 0, 0);
    }

    // Initialization
    void Start()
    {
        //serialController = GameObject.Find("SerialController").GetComponent<SerialController>();

        Debug.Log("Press A or Z to execute some actions");
    }

    // Executed each frame
    void Update()
    {
        //---------------------------------------------------------------------
        // Send data
        //---------------------------------------------------------------------

        // If you press one of these keys send it to the serial device. A
        // sample serial device that accepts this input is given in the README.
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Sending A");
            serialController.SendSerialMessage("A");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Sending Z");
            serialController.SendSerialMessage("Z");
        }


        //---------------------------------------------------------------------
        // Receive data
        //---------------------------------------------------------------------

        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Connection attempt failed or disconnection detected");
        else
        {
            Debug.Log("Message arrived: " + message);
            Message = message;
            MessageSplit = Message.Split('|');

            oldRot = obj.eulerAngles;
            obj.eulerAngles = new Vector3(-float.Parse(MessageSplit[5]), float.Parse(MessageSplit[3]), float.Parse(MessageSplit[4]));

            newRot = obj.eulerAngles;
            newRot = new Vector3(newRot.x > 0 ? newRot.x : (360 + newRot.x), newRot.y > 0 ? newRot.y : (360 + newRot.y), newRot.z > 0 ? newRot.z : (360 + newRot.z));

            differenceX = obj.eulerAngles.x - oldRot.x;
            differenceY = obj.eulerAngles.y - oldRot.y;

            if (oldRot.x < 180 && newRot.x > 180)
                differenceX = 360 - differenceX;
            if (oldRot.y < 180 && newRot.y > 180)
                differenceY = 360 - differenceY;

            if (oldRot.x > 180 && newRot.x < 180)
                differenceX = differenceX + 360;
            if (oldRot.y > 180 && newRot.y < 180)
                differenceY = differenceY + 360;

            Move((int)(differenceY * 0.05f), (int)(differenceX * 0.05f));
        }
    }


    void SplitFunction() {
        MessageSplit = Message.Split('|');

        Debug.Log($"Message splited is: [0] {MessageSplit[0]} , [1] {MessageSplit[1]} , [2] {MessageSplit[2]} ");

        Debug.Log($"Message splited is: [3] {MessageSplit[3]} , [4] {MessageSplit[4]} , [5] {MessageSplit[5]} ");

        Accelerometer = new Vector3(float.Parse(MessageSplit[0], CultureInfo.InvariantCulture.NumberFormat),
                                    float.Parse(MessageSplit[1], CultureInfo.InvariantCulture.NumberFormat),
                                    float.Parse(MessageSplit[2], CultureInfo.InvariantCulture.NumberFormat));

        Gyroscope = new Vector3(float.Parse(MessageSplit[3], CultureInfo.InvariantCulture.NumberFormat),
                                   float.Parse(MessageSplit[4], CultureInfo.InvariantCulture.NumberFormat),
                                   float.Parse(MessageSplit[5], CultureInfo.InvariantCulture.NumberFormat));


        ObjectToMove.transform.position = Accelerometer;

        ObjectToMove.transform.rotation = Quaternion.Euler(Gyroscope);

        for (int i = 0; i < MessageSplit.Length; i++)
        {

        }
    }
}
