using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Net;
using System.IO;
using UnityEngine.UI;

public class Server : MonoBehaviour
{
    [SerializeField] int port = 41222;

    private List<ServerClient> clients ;
    private List<ServerClient> disconnectedList;
    
    public InputField PortInput;

    public GameObject WaitingPlayersPopUp;

    private bool _gameStarted=false;

    private TcpListener server;
    private bool serverStarted;

    private void Start()
    {
     
    }

    public void StartServer() {
        clients = new List<ServerClient>();
        disconnectedList = new List<ServerClient>();

        try
        {
            try
            {
                server = new TcpListener(IPAddress.Any, int.Parse(PortInput.text));
            }
            catch (Exception e) {
                Debug.Log(e.Message +" porta inválida");
                server = new TcpListener(IPAddress.Any, port);
            }
            server.Start();

            StartListening();
            serverStarted = true;

        Debug.Log("Server started at port " + port.ToString());
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void Update()
    {
        if (!serverStarted)
            return;

        foreach (ServerClient c in clients)
        {
            //is the client still connected?
            //Check for messages
            if (!IsConnected(c.tcp))
            {             
                disconnectedList.Add(c);
                Broadcast(c.clientName + " has disconnected", clients);
                clients.Remove(c);
                c.tcp.Close(); //Close socket
                break;
            }
            else {
                NetworkStream stream = c.tcp.GetStream();
                if (stream.DataAvailable) {
                    StreamReader reader = new StreamReader(stream, true);
                    string data = reader.ReadLine();

                    if (!string.IsNullOrEmpty(data)) {
                        OnIncomingData(c, data);
                    }
                }

            }
        }
        for (int i = 0; i < disconnectedList.Count - 1; i++)
        {
            Broadcast(disconnectedList[i].clientName + " has disconnected", clients);

            clients.Remove(disconnectedList[i]);
            disconnectedList.RemoveAt(i);
        }

      
    }

    public void StartGame() {
        foreach (ServerClient c in clients)
        {
            Broadcast(c.clientName + "%STARTGAME", clients);
        }
    }

    private void OnIncomingData(ServerClient c, string data)
    {
        if (data.Contains("&NAME")) {
            c.clientName = data.Split('|')[1];
            Debug.Log(c.clientName + " has connected!!!!");
            Debug.Log(clients.Count.ToString());
            Broadcast(c.clientName + " has connected", clients);
            Broadcast(clients.Count.ToString(),clients);

            if (clients.Count > 1 )
            {
                WaitingPlayersPopUp.SetActive(false);
                if (!_gameStarted)
                {
                    _gameStarted = true;
                    Debug.Log("STARTED!!!!");
                    StartGame();
                }
            }
            return;
        }
        //&STARTGAME|
        if (data.Contains("&STARTGAME"))
        {            
            Broadcast("game start", clients);
            return;
        }
        Debug.Log(c.clientName + " sent: " + data);
        //Broadcast("<b>"+c.clientName + ": </b>"+data, clients);
        Broadcast(c.clientName + ": "+data,clients);
    }

    private bool IsConnected(TcpClient c)
    {
        try {
            if (c != null && c.Client != null && c.Client.Connected) {
                if (c.Client.Poll(0, SelectMode.SelectRead)) {
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }
                return true;
            }else return true;
        } catch {
            return false;
        }
    }

    private void StartListening()
    {
        server.BeginAcceptTcpClient(AcceptTCPClient, server);
    }
     

    private void AcceptTCPClient(IAsyncResult ar) {
        TcpListener listener = (TcpListener)ar.AsyncState;
        clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));
        StartListening();

        //send a message to everyone, say someone has connected;        
        Broadcast("%NAME", new List<ServerClient> { clients[clients.Count - 1] });
        Broadcast(clients.Count.ToString(), new List<ServerClient> { clients[clients.Count - 1] });
    }

    private void Broadcast(string data, List<ServerClient> cli) {
        foreach(ServerClient c in cli){
            try {
                StreamWriter writer = new StreamWriter(c.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();

            } catch (Exception e) {
                Debug.Log("Writer error: " + e.Message + "to client " + c.clientName);
            }
        }

    }
}

[System.Serializable]
public class ServerClient {
    public TcpClient tcp;
    public string clientName;

    public ServerClient(TcpClient clientSocket) {
        clientName = "";
        tcp = clientSocket;
    }
}
