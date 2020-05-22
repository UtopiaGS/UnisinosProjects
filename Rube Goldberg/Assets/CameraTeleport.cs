using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraTeleport : MonoBehaviour
{

    public List<GameObject> CameraPositions = new List<GameObject>();
    public static UnityEvent Cut;
    int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;

        if (Cut == null)
            Cut = new UnityEvent();

        Cut.AddListener(ChangeToNextCut);

       ChangeToNextCut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeToNextCut() {
        Debug.Log("Cut camera index "+currentIndex+ " Count > "+ CameraPositions.Count);
       for (int i = 0; i < CameraPositions.Count; i++)
        {
            if (i == currentIndex)
            {
                Debug.Log("Ativando camera na pos current index " + i);
                CameraPositions[i].gameObject.SetActive(true);
               
            }
            else {
                CameraPositions[i].gameObject.SetActive(false);
            }
        }
        currentIndex += 1;
    }
}
