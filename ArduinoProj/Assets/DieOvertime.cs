using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOvertime : MonoBehaviour
{
    public float TimeToDie=3;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, TimeToDie);
    }

}
