using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPosition : MonoBehaviour
{
    private float _startPositionY;
    public float UnitsDown = 5;
    private float _fallenPosition;



    private bool HasFallen;
    // Start is called before the first frame update
    void Start()
    {
        HasFallen = false;
        _startPositionY = transform.position.y;
        _fallenPosition = _startPositionY - UnitsDown;
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"{gameObject.name} Start pos is { _startPositionY } and _start - units is { _fallenPosition }");
        if (!HasFallen && transform.position.y < _fallenPosition) {

            Debug.Log(gameObject.name + " HAS FALLEN!!!!");
            HasFallen = true;
            RoundsController.Instance.TargetDownObserver.Invoke();
        }
    }
}
