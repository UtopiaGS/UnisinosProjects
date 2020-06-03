﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddForce : MonoBehaviour
{
    public Rigidbody rb;
    public HingeJoint rope;
    public Slider slider;
    public Text forceTxt;

    public static AddForce Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        forceTxt.text = slider.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeForceText() {
        forceTxt.text = slider.value.ToString();
        rb.mass = slider.value;
    }

    public void UpdateReferences(Rigidbody weight, HingeJoint Rope) {
        rope = Rope;
        rb = weight;
    }
    public void ApplyForceAndBreak() {
        if (rope != null)
        {
            rb.mass = slider.value;
            rope.breakForce = 0;
            rb.AddForce(0, -(slider.value), 0);
            Debug.Log("Throwing!");
            StartCoroutine(CanvasController.Instance.WaitForEnableNext());
        }
    }
}
