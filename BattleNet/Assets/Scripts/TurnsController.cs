using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnsController : MonoBehaviour
{
    public static TurnsController instance;
    public List<PlayerCharacters> Players = new List<PlayerCharacters>();
    private int indexTurn = 1;
    public int IndexCharacter => indexTurn;
    PlayerCharacters _currentPlayer;
    Character _currentCharacter;
    private Character _currentTarget;

    public List<Character> AllCharacters;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void ClearTurn() {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].UpdateOutlines();
        }
    }

    public void SetCurrentTarget(Character target) {
        _currentTarget = target;
    }

   public void ChangeTurn(int turn, Client cli) {
        ClearTurn();
        indexTurn = turn;
        Debug.Log(turn);
        _currentPlayer = Players[indexTurn];

        Debug.Log(_currentPlayer.gameObject.name + "  " + indexTurn);

        int curChar = _currentPlayer.CharacterTurn();

        cli.Send(CommandReader.SendCommand(Command.START_TURN, Sender.CLIENT, _currentPlayer.ID.ToString(), curChar.ToString(), "0", 0));
        Debug.Log("SEND TURN START CALLED!!!");
    }




    public void AttackTarget() {
        _currentPlayer.SendAttackMessage(_currentTarget);
        //_currentPlayer.ChracterTurn.MoveToTarget(_currentTarget);
    }

  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // ChangeTurn();
        }
    }
}
