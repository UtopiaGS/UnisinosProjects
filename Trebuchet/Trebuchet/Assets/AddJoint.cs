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
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (gameObject.GetComponent<HingeJoint>() == null)
            {
                HingeJoint joint = gameObject.AddComponent<HingeJoint>();
                joint.anchor = _anchor;
                joint.connectedBody = _body;
                _body.isKinematic = true;
            
                AddForce.Instance.rope = joint;
                // _body.isKinematic = false;
              
               
                StartCoroutine(WaitForEnableRigidbody());
            }

        }
    }

    IEnumerator WaitForEnableRigidbody() {
        yield return new WaitForSeconds(0.5f);
      
        _body.velocity = new Vector3(0f, 0f, 0f);
        _body.angularVelocity = new Vector3(0f, 0f, 0f);
            Aim.transform.rotation = _aimTransform;
        _body.isKinematic = false;
    }
}
