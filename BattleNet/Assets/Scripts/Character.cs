using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject RootGameObject;
    private List<CharacterSelection> _shaderOutline = new List<CharacterSelection>();
    private List<CapsuleCollider> _colliders = new List<CapsuleCollider>();
    public float timeToMove;
    public GameObject Target;
    private Vector3 _startPos;
    private Quaternion _startRotation;
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _startRotation = transform.rotation;
        int numOfChildren = transform.childCount;//compte le nombre d'enfant du GameObject
        int i = 0;
        List<GameObject> goChild = new List<GameObject>();
        
        for (i = 0; i < numOfChildren; i++)
        {
            _shaderOutline.Add(GetComponentInChildren<CharacterSelection>());
            _colliders.Add(GetComponentInChildren<CapsuleCollider>());
        }
    }

    public void SelectCharacter() {
        for (int i = 0; i < _shaderOutline.Count; i++)
        {
            _shaderOutline[i].SetSelectionColor();
        }
        for (int i = 0; i < _colliders.Count; i++)
        {
            _colliders[i].enabled = false;
        }
    }

    public void EnableColliders(bool activation) {

        for (int i = 0; i < _colliders.Count; i++)
        {
            _colliders[i].enabled = activation;
        }
    }

    public void EndOwnerTurn() {
        for (int i = 0; i < _shaderOutline.Count; i++)
        {
            _shaderOutline[i].CleanSelection();
        }
        for (int i = 0; i < _colliders.Count; i++)
        {
            _colliders[i].enabled = true;
        }
    }

    public void MoveToTarget(Vector3 endPos) {
        transform.LookAt(endPos);
        StartCoroutine(MoveToTarget(transform.position, endPos, transform, timeToMove));
    }

    IEnumerator MoveToTarget(Vector3 startPos, Vector3 endPos, Transform actor, float time)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {           
            // Debug.Log("elapsed time" + elapsedTime);
            elapsedTime += Time.deltaTime;
            actor.position = Vector3.Lerp(startPos, endPos, (elapsedTime / time));

            yield return null;
        }
        yield return new WaitForSeconds(1f);
        transform.LookAt(_startPos);
        endPos = _startPos;
        startPos = transform.position;
        elapsedTime = 0;
        while (elapsedTime < time)
        {
            // Debug.Log("elapsed time" + elapsedTime);
            elapsedTime += Time.deltaTime;
            actor.position = Vector3.Lerp(startPos, endPos, (elapsedTime / time));

            yield return null;
        }
        transform.rotation=_startRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            //Debug.Log("OIAFIOHFA");
            //transform.LookAt(Target.transform);
            //MoveToTarget(Target.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
           // Debug.Log("OIAFIOHFA");
           // transform.LookAt(_startPos);
           // MoveToTarget(_startPos);
        }
    }

    void OnMouseDown()
    {
       // Debug.Log("ENTROU DISGRAÇA");
        //if (Input.GetKeyDown(KeyCode.Mouse0)){
            // Whatever you want it to do.
            TurnsController.instance.AttackTarget(transform.position);
       // }
      
    }

    void OnMouseExit()
    {
       
    }
}
