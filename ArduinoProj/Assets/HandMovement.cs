using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HandMovement : MonoBehaviour
{
    public Camera CameraPosition;
    Vector3 originalPos;
    public Vector3 InputXYZ;

    public float velocity = 2;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            InputXYZ.y+= velocity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            InputXYZ.x-= velocity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            InputXYZ.y-= velocity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            InputXYZ.x+= velocity * Time.deltaTime;
        }

        transform.position = originalPos + InputXYZ;

    }
}
