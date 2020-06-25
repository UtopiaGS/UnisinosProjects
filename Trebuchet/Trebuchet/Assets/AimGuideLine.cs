using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimGuideLine : MonoBehaviour
{
    public Transform[] arrayTransform;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = arrayTransform.Length;
        for (int i = 0; i < arrayTransform.Length; i++)
        {
            lineRenderer.SetPosition(i, arrayTransform[i].position);
        }
    }
}
