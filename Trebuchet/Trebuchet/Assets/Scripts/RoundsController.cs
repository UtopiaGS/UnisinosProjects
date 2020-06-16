using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsController : MonoBehaviour
{
    public TrebuchetComponets TrebuchetPrefab;
    public static RoundsController Instance;
    public Vector3 position;
    private TrebuchetComponets _currentTrebuchet;
    public TrebuchetComponets CurrentTrebuchet => _currentTrebuchet;

    private int _rounds = 0 ;
    public int Rounds => _rounds;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateNewTrebuchet();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void InstantiateNewTrebuchet() {
        TrebuchetComponets clone;
        clone = Instantiate(TrebuchetPrefab, position, Quaternion.identity);

        CameraFollowObject.Instance.TargetObject = clone.StoneTarget;
        _currentTrebuchet = clone;

        _rounds++;

    }

    public void DestroyAndCreateTrebuchet() {
        StartCoroutine(DestroyAndCreateRoutine(1.0f));
    }

    private IEnumerator DestroyAndCreateRoutine(float delay ) {
        yield return new WaitForSeconds(delay);
        DestroyTrebuchet();
        yield return new WaitForSeconds(0.1f);
        InstantiateNewTrebuchet();
    }


    private void DestroyTrebuchet() {
        Destroy(_currentTrebuchet.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
