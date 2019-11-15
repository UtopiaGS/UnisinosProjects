using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacters : MonoBehaviour
{
    public int ID;
    public List<Character> Characters = new List<Character>();

    public PlayerCharacters Oponent;

    private int _indexTurn=2;
    public int IndexCharacter => _indexTurn;

    private Character _characterTurn;
    public Character ChracterTurn=> _characterTurn;
    public Client PlayerClient;

    private string _playerID;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            Characters[i].OwnerID = ID;
            Characters[i].Owner = this;
            Characters[i].ID = i;
        }
          
        

        PlayerClient.Player = this;
    }
        

    public int CharacterTurn() {
       // EndTurn();
        SetCollidersActivation(false);
        _indexTurn++;
        _indexTurn = _indexTurn % Characters.Count;
        Debug.Log(_indexTurn);
        Character currentCharacter = Characters[_indexTurn];
        currentCharacter.SelectCharacter();
        _characterTurn = currentCharacter;

        Debug.Log(currentCharacter.gameObject.name +"  "+_indexTurn);
        return _indexTurn;
    }

    public void EndTurn() {
        
        SetCollidersActivation(true);
        string endTurn = CommandReader.SendCommand(Command.END_TURN, Sender.CLIENT, ID.ToString(), _characterTurn.ID.ToString(),"", 0);
        PlayerClient.OnSendButton(endTurn);
       
    }

    public void UpdateOutlines() {
        for (int i = 0; i < Characters.Count; i++)
        {
            Characters[i].EndOwnerTurn();
        }
    }

    public void SendAttackMessage(Character currentTarget) {

        string attack = CommandReader.SendCommand(Command.ATTACK, Sender.CLIENT, ID.ToString(), _characterTurn.ID.ToString(),currentTarget.ID.ToString(), 0);
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
