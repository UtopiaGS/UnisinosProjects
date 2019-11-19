using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Command
{
    END_TURN, 
    ATTACK,
    START_TURN,
    UPDATE_STAT,
    START_MATCH,
    END_MATCH,
    DEAD_CHAR,
    PLAYER_WINNER,
    CURRENT_PLAYER,
}

public enum Sender {
    SERVER,
    CLIENT,
}


// % - SERVER PARA CLIENTS

    //& - DE CLIENTES PARA O SERVER. 

public class CommandReader : MonoBehaviour
{
    public static CommandReader instance;
    public Server Server;
    public PlayerCharacters Player1;
    public PlayerCharacters Player2;


    public void Awake()
    {
        instance = this;
    }

    public void ClientReadCommand(Client cli, string data) {

        Debug.Log(data);
        if (data == "%NAME")
        {
            Debug.Log("CLIENT NAME >> " + cli.ClientName);
            cli.Send("&NAME|" + cli.ClientName);
           
            return;
        }

        if (data.Contains("%ADDPLAYER"))
        {
            Debug.Log("CLIENT NAME >> " + cli.ClientName);
            cli.Send("&ADDPLAYER|" + cli.ClientName +"?"+cli.Player.ID+"-");
            return;
        }

        if (data.Contains("%NAMEPLAYER"))
        {
            //cli.Send("&ADDPLAYER|" + cli.ClientName + "?" + cli.Player.ID + "-");
            string name = GetStringInBetweenString(data, "|", "?");
            int id = GetIdInBetweenString(data, "?", "-");
            Debug.Log("SET NAMEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE>> " +id+ "___" + cli.ClientName );
            TurnsController.instance.SetPlayerNameText(name, id);
            return;
        }

        if (data == "%STARTGAME|0")
        {
            Debug.Log("START GAME CHANGE CALLL>> " + data);
            cli.Send("&STARTGAME");
            TurnsController.instance.ChangeTurn(0,cli);

           
            return;
        }

        if (data.Contains("%END_TURN>0"))
        {           
            TurnsController.instance.ChangeTurn(1,cli);
            return;
        }
        if (data.Contains("%END_TURN>1"))
        {
            TurnsController.instance.ChangeTurn(0, cli);
            return;
        }

        if (data.Contains("%ATTACK>")) {
            Debug.Log("COMMAND READER GOT THAT ATTACK");

            PlayerCharacters owner;

            int player = GetIdInBetweenString(data, ">", "|");
            if (player == 0)
            {
                owner = Player1;
            }
            else {
                owner = Player2;
            }
            Debug.Log("PLAYER TO ATTACK ____ " + player);

            int character = GetIdInBetweenString(data, "|", "+");

            Debug.Log("CHARACTER TO ATTACK ____ " + character);

            int target = GetIdInBetweenString(data, "+", "?");

            Debug.Log("TARGET ____ " + target);

            int damage = GetIdInBetweenString(data, "?", "#");

            Debug.Log("DAMAGE ____ " + damage);

            owner.Characters[character].MoveToTarget(owner.Oponent.Characters[target],damage);
            return;
        }
    }

    private int GetIdInBetweenString(string text, string separatorLeft, string separetorRight) {
       string result = GetStringInBetweenString(text, separatorLeft, separetorRight);
       return int.Parse(result);
    }

    private string GetStringInBetweenString(string text, string separatorLeft, string separetorRight)
    {
        int pFrom = text.IndexOf(separatorLeft) + separatorLeft.Length;
        int pTo = text.LastIndexOf(separetorRight);

        string result = text.Substring(pFrom, pTo - pFrom);

        return result;
    }

    public void ServerReadCommand(Server _server, ServerClient serverClients, string data) {
        if (data.Contains("&NAME"))
        {
            serverClients.clientName = data.Split('|')[1];
            if (_server.Clients.Count == 1)
            {
                serverClients.ID = 0;
            }
            else if (_server.Clients.Count == 2) {
                serverClients.ID = 1;
            }
            _server.Broadcast(serverClients.clientName + " has connected", _server.Clients);
            _server.Broadcast("Clients connected>>>" + _server.Clients.Count.ToString(), _server.Clients);

            return;
        }
        //&STARTGAME|
        if (data == "&STARTGAME")
        {
            _server.Broadcast("START", serverClients);

            //cli.Send("&ADDPLAYER|" + cli.ClientName + "?" + cli.Player.ID + "-");

            return;
        }
        Debug.Log(serverClients.clientName + " sent: " + data);
        //Broadcast("<b>"+c.clientName + ": </b>"+data, clients);
        //_server.Broadcast(serverClients.clientName + ": " + data, _server.Clients);

        if (data.Contains("&ATTACK>")) {
            string attack = data;
            attack = attack.Replace("&", "%");

            PlayerCharacters owner;

            int player = GetIdInBetweenString(data, ">", "|");
            if (player == 0)
            {
                owner = Player1;
            }
            else
            {
                owner = Player2;
            }
            Debug.Log("PLAYER TO ATTACK ____ " + player);

            int character = GetIdInBetweenString(data, "|", "+");

            Debug.Log("CHARACTER TO ATTACK ____ " + character);

            int target = GetIdInBetweenString(data, "+", "?");

            Debug.Log("TARGET ____ " + target);
            
            int damage = owner.Characters[character].CalculateDamage();
            attack += damage.ToString() + "#";
            Debug.Log("ATTAAAAAAAAACKKKKKKK!!!");
            Debug.Log("Server Broadcast: "+attack);
            _server.Broadcast(attack, _server.Clients);
            return;
        }


        if (data.Contains("&END_TURN>"))
        {
            string attack = data;
            attack = attack.Replace("&", "%");
            Debug.Log(attack);
            _server.Broadcast(attack, _server.Clients);
            return;
        }

        if (data.Contains("&ADDPLAYER"))
        {
            string addName = data;
            addName = addName.Replace("&ADDPLAYER", "%NAMEPLAYER");

            string name = GetStringInBetweenString(data, "|", "?");
            int id = GetIdInBetweenString(data, "?", "-");

            Debug.Log(addName);
            _server.Broadcast(addName, _server.Clients);

            foreach (ServerClient c in Server.Clients)
            {
                //cli.Send("&ADDPLAYER|" + cli.ClientName + "?" + cli.Player.ID + "-");
                _server.Broadcast("%NAMEPLAYER|" + c.clientName + "?" + c.ID + "-", _server.Clients);
            }
            return;
        }

    }


    public static string SendCommand(Command Cmd, Sender sender, string PlayerID, string CharacterID, string targetID, int stat) { 
        string cmd = string.Empty;

        switch (sender) {
            case Sender.CLIENT:
                cmd = "&";
                break;
            case Sender.SERVER:
                cmd = "%";
                break;
        }

        switch (Cmd) {
            case Command.END_TURN:
                cmd += string.Concat("END_TURN>", PlayerID , "|", CharacterID);
                break;
            case Command.ATTACK:
                cmd += string.Concat("ATTACK>", PlayerID, "|", CharacterID, "+", targetID,"?");
                break;
            case Command.DEAD_CHAR:
                cmd += string.Concat("DEAD>", PlayerID, "|", CharacterID);
                break;
            case Command.END_MATCH:
                cmd += string.Concat("END_MATCH>", PlayerID);
                break;
            case Command.PLAYER_WINNER:
                cmd += string.Concat("WINNER>", PlayerID);
                break;
            case Command.START_MATCH:
                cmd += string.Concat("START_MATCH");
                break;
            case Command.START_TURN:
                cmd += string.Concat("START_TURN>", PlayerID, "|", CharacterID);
                break;
            case Command.UPDATE_STAT:
                cmd += string.Concat("UPDATE_STAT>", PlayerID, "|", CharacterID, "=", stat.ToString());
                break;
        }
        
        return cmd;
    }

    
}
