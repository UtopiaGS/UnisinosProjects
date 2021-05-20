using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnOnCollision : MonoBehaviour
{
    public GameObject instanciatedPrefab;
    bool _wasCollided = false;
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
        if (!_wasCollided)
        {
            Instantiate(instanciatedPrefab, transform.position, transform.rotation);
            _wasCollided = true;
        }
    }
}
