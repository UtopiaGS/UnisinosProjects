using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFireDetection : MonoBehaviour
{
    public bool isOnFire = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fire")) {
            Debug.Log("THIS TARGET IS ON FIREEEEEEEEEEEEEE");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            isOnFire = true;
            var cubeRenderer = gameObject.GetComponent<Renderer>();
            cubeRenderer.material.SetColor("_Color", Color.red);
            Debug.Log("THIS TARGET IS ON FIREEEEEEEEEEEEEE");
        }
    }
}
