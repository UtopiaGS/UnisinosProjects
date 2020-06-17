using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundsController : MonoBehaviour
{
    public TrebuchetComponets TrebuchetPrefab;
    public static RoundsController Instance;
    public Vector3 position;
    private TrebuchetComponets _currentTrebuchet;
    public TrebuchetComponets CurrentTrebuchet => _currentTrebuchet;

    private int _rounds = 0 ;
    public int Rounds => _rounds;

    private int _targetsDown = 0;

    public UnityEvent TargetDownObserver = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        _targetsDown = 0;
        InstantiateNewTrebuchet();
        TargetDownObserver.AddListener(TargetDownScore);


    }

    private void Awake()
    {
        Instance = this;
    }

    private void TargetDownScore() {
        _targetsDown++;
       // SoundPlayer.Instance.PlayClipId();
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
        if (_targetsDown <= 4) {
            Debug.Log("VENCEEEEEEEEEEEEEEEEEEEEEEEEEEEEEUUUUUUUUUUUUUUUUU");
        }
    }
}
