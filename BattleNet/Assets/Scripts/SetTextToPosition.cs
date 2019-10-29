using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextToPosition : MonoBehaviour
{
    public Slider slider;
    public GameObject ObjectToFollow;
    public Vector3 Offset;
    // Use this for initialization

    private void Start()
    {
        ObjectToFollow.GetComponent<Character>().Slider = slider;
    }

    private void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(ObjectToFollow.transform.position+ Offset);
        slider.transform.position = pos;
    }
    
}
