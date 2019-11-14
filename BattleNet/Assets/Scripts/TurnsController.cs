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

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void ClearTurn() {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].EndTurn();
        }
    }

    public void SetCurrentTarget(Character target) {
        _currentTarget = target;
    }

   public void ChangeTurn() {
        ClearTurn();
        indexTurn++;
        indexTurn = indexTurn % Players.Count;
        Debug.Log(indexTurn);
        _currentPlayer = Players[indexTurn];

        Debug.Log(_currentPlayer.gameObject.name + "  " + indexTurn);

        _currentPlayer.CharacterTurn();
        _currentPlayer.SendStartTurn();
        Debug.Log("SEND TURN START CALLED!!!");
    }

    public void AttackTarget() {
        _currentPlayer.SendAttackMessage(_currentTarget);
        _currentPlayer.ChracterTurn.MoveToTarget(_currentTarget.transform.position);
    }

  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeTurn();
        }
    }
}
