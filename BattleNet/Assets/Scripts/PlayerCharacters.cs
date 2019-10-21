using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacters : MonoBehaviour
{
    public List<Character> Characters = new List<Character>();
    private int _indexTurn=2;
    public int IndexCharacter => _indexTurn;

    private Character _characterTurn;
    public Character ChracterTurn=> _characterTurn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CharacterTurn() {
        EndTurn();
        SetCollidersActivation(false);
        _indexTurn++;
        _indexTurn = _indexTurn % Characters.Count;
        Debug.Log(_indexTurn);
        Character currentCharacter = Characters[_indexTurn];
        currentCharacter.SelectCharacter();
        _characterTurn = currentCharacter;

        Debug.Log(currentCharacter.gameObject.name +"  "+_indexTurn);

    }

    public void EndTurn() {
        for (int i = 0; i < Characters.Count; i++)
        {
            Characters[i].EndOwnerTurn();
        }
        SetCollidersActivation(true);
    }

    public void SetCollidersActivation(bool act) {
        for (int i = 0; i < Characters.Count; i++)
        {
            Characters[i].EnableColliders(act);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
