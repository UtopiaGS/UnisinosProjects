using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelaseProjectile : MonoBehaviour
{
    public GameObject Target;
    private FixedJoint targetJoint;
    // Start is called before the first frame update
    void Start()
    {
       targetJoint =  Target.GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Target) {
            targetJoint.breakForce = 0;
        }
    }
}
