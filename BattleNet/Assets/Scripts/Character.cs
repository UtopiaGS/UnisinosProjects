using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject RootGameObject;
    private List<CharacterSelection> _shaderOutline = new List<CharacterSelection>();
    private List<CapsuleCollider> _colliders = new List<CapsuleCollider>();
    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
