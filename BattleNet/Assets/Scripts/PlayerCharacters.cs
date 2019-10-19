using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacters : MonoBehaviour
{
    public List<Character> Characters = new List<Character>();
    private int indexTurn=2;
    public int IndexCharacter => indexTurn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CharacterTurn() {
        EndTurn();
        SetCollidersActivation(false);
        indexTurn++;
        indexTurn = indexTurn % Characters.Count;
        Debug.Log(indexTurn);
        Character currentCharacter = Characters[indexTurn];
        currentCharacter.SelectCharacter();

        Debug.Log(currentCharacter.gameObject.name +"  "+indexTurn);

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
