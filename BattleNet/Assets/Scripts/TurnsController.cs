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

   public  void ChangeTurn() {
        ClearTurn();
        indexTurn++;
        indexTurn = indexTurn % Players.Count;
       // Debug.Log(indexTurn);
        _currentPlayer = Players[indexTurn];

       // Debug.Log(_currentPlayer.gameObject.name + "  " + indexTurn);

        _currentPlayer.CharacterTurn();
    }

    public void AttackTarget(Vector3 endPos) {

        Debug.Log("FOI DESGRAÇA");
        _currentPlayer.ChracterTurn.MoveToTarget(endPos);

    }

    private void Start()
    {
        ChangeTurn();
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
