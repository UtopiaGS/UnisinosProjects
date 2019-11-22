using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnsController : MonoBehaviour
{
    public static TurnsController instance;
    public List<PlayerCharacters> Players = new List<PlayerCharacters>();
    private int indexTurn = 1;
    public int IndexCharacter => indexTurn;
    PlayerCharacters _currentPlayer;
    Character _currentCharacter;
    private Character _currentTarget;

    public List<Text> PlayersName;
    public List<Image> PlayerArrow;

    public GameObject WinnerText;
    public GameObject BackToMenu;

    public PlayerCharacters instancePlayer;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        BackToMenu.SetActive(false);

        for (int i = 0; i < PlayerArrow.Count; i++)
        {
            PlayerArrow[i].enabled = false;
        }
    }

    public void ClearTurn()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].UpdateOutlines();
        }
    }

    public void SetCurrentTarget(Character target)
    {
        _currentTarget = target;
    }

    public void SetPlayerNameText(string name, int id) {
        PlayersName[id].text = name;
    }

    public void BlockMouseSelection(int id)
    {
        if (instancePlayer.ID != id)
        {
            for (int i = 0; i < Players.Count; i++)
            {
                for (int j = 0; j < Players[i].Characters.Count; j++)
                {
                    if (Players[i].Characters[j] != null)
                    {
                        //Debug.Log("Desabilita todos colisores");
                        Players[i].Characters[j].EnableColliders(false);
                    }
                }
            }
        }
        if (instancePlayer.ID == id)
        {
            for (int i = 0; i < Players.Count; i++)
            {
                if (instancePlayer != Players[i]) {
                    for (int j = 0; j < Players[i].Characters.Count; j++)
                    {
                        if (Players[i].Characters[j] != null)
                        {
                            // Debug.Log("Habilita colisor dos adversários");
                            Players[i].Characters[j].EnableColliders(true);
                        }
                    }
                }
            }
        }

    }

    public void CheckWinner() {
        int loserID=-1;
        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i].Characters.Count <= 0) {
                loserID = i;
            }

        }
        if (loserID != -1) {
            if (loserID == 0) {
                WinnerText.GetComponent<Text>().text = PlayersName[1].text + " is the winner!";
                BackToMenu.SetActive(true);
            } else if (loserID == 1) {

                WinnerText.GetComponent<Text>().text = PlayersName[0].text + " is the winner!";
                BackToMenu.SetActive(true);
            }
        }

    }

    public void ChangeTurn(int turn, Client cli)
    {
        SetPlayerArrow(turn);
        CheckWinner();
        ClearTurn();
        indexTurn = turn;
        _currentPlayer = Players[indexTurn];

        Debug.Log(_currentPlayer.gameObject.name + "  " + indexTurn);

        int curChar = _currentPlayer.CharacterTurn();

        cli.Send(CommandReader.SendCommand(Command.START_TURN, Sender.CLIENT, _currentPlayer.ID.ToString(), curChar.ToString(), "0", 0));
        Debug.Log("SEND TURN START CALLED!!!");

        BlockMouseSelection(turn);
    }


    public void SetPlayerArrow(int index) {
        if (index == 0) {
            PlayerArrow[0].enabled = true;
            PlayerArrow[1].enabled = false;
        }
        if (index == 1)
        {
            PlayerArrow[0].enabled = false;
            PlayerArrow[1].enabled = true;
        }
    }



    public void AttackTarget()
    {
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
