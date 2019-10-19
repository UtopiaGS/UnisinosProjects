using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnsController : MonoBehaviour
{
    public List<PlayerCharacters> Players = new List<PlayerCharacters>();
    private int indexTurn = 1;
    public int IndexCharacter => indexTurn;
    // Start is called before the first frame update
    void Start()
    {
        
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
        Debug.Log(indexTurn);
        PlayerCharacters currentPlayer = Players[indexTurn];

        Debug.Log(currentPlayer.gameObject.name + "  " + indexTurn);

        currentPlayer.CharacterTurn();
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
