using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacters : MonoBehaviour
{
    public string ID;
    public List<Character> Characters = new List<Character>();
    private int _indexTurn=2;
    public int IndexCharacter => _indexTurn;

    private Character _characterTurn;
    public Character ChracterTurn=> _characterTurn;
    public Client PlayerClient;
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
        string endTurn = string.Concat("%ENDTURN ", ID, " ", _characterTurn.ID);
        PlayerClient.OnSendButton(endTurn);
    }

    public void SendAttackMessage(Character currentTarget) {

        string attack = string.Concat("%ATTACK ", ID, " +", _characterTurn.ID, ">", currentTarget.ID);
        PlayerClient.OnSendButton(attack);
    }
    public void SendStartTurn()
    {
        string attack = string.Concat("%TURN| ", ID, " +", _characterTurn.ID, ">", _characterTurn.ID);
        PlayerClient.OnSendButton(attack);
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
