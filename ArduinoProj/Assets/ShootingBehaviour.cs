using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ShootingBehaviour : MonoBehaviour
{
    public GameObject Projectile;
    public GameObject playerPos;
    public float Force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Shooting();
        }
    }

    public void Shooting() {
        GameObject projectile = Instantiate(Projectile, playerPos.transform.position, playerPos.transform.rotation);
        var rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(-playerPos.transform.forward * Force, ForceMode.Force);
        }

    }
}
