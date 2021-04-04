using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ShootingBehaviour : MonoBehaviour
{
    public GameObject Projectile;
    public FirstPersonController _fpsController;
    public float Force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            GameObject projectile = Instantiate(Projectile, transform.position, _fpsController.transform.rotation);
            var rb = projectile.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddForce(_fpsController.transform.forward * Force, ForceMode.Force);
            }
        }
    }
}
