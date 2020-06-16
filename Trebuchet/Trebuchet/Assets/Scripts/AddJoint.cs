using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddJoint : MonoBehaviour
{
    public HingeJoint jointModel;
    private Vector3 _anchor;
    private Rigidbody _body;
    public GameObject Aim;
    public Quaternion _aimTransform;
    // Start is called before the first frame update
    void Start()
    {
        jointModel = GetComponent<HingeJoint>();
        _anchor = jointModel.anchor;
        _body = jointModel.connectedBody;
        _aimTransform = Aim.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
