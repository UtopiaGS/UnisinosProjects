using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallCut : MonoBehaviour
{
    bool _hasTriggered=false;
    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        _hasTriggered=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject==Target && Target!=null) {
            if (!_hasTriggered)
            {
                _hasTriggered = true;
                Debug.Log("COLIDIU EM "+collision.gameObject.name);
                if (CameraTeleport.Cut != null)
                {
                    CameraTeleport.Cut.Invoke();
                }
            }
        }
    }
}
