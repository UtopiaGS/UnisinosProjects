using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextToPosition : MonoBehaviour
{
    public Slider slider;
    public Transform position;
    public Vector3 Offset;
    // Use this for initialization
   
    private void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(position.position+ Offset);
        slider.transform.position = pos;
    }
    
}
