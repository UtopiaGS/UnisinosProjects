using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFireworks : MonoBehaviour
{
    bool _hasTriggered=false;
    public GameObject Target;
    public List<GameObject> GameObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        _hasTriggered=false;

        for (int i = 0; i < GameObjects.Count; i++)
        {
            GameObjects[i].SetActive(false);
        }
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
                for (int i = 0; i < GameObjects.Count; i++)
                {
                    GameObjects[i].SetActive(true);
                }
            }
        }
    }
}
