using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Net;
using System.IO;

public class Server : MonoBehaviour
{
    [SerializeField] int port = 6321;

    private List<ServerClient> clients;
    private List<ServerClient> disconnectedList;

    private TcpListener server;
    private bool serverStarted;

    private void Start()
    {
        clients = new List<ServerClient>();
        disconnectedList = new List<ServerClient>();

        try {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();

            StartListening();
            serverStarted = true;

        } catch(Exception e) {
            Debug.Log(e.Message);
        }
        Debug.Log("Server started at port " + port.ToString());
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
                c.tcp.Close(); //Close socket
                disconnectedList.Add(c);
                continue;
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
    }

    private void OnIncomingData(ServerClient c, string data)
    {
        //Debug.Log(c.clientName + " sent: " + data);
        Broadcast("<b>"+c.clientName + ": </b>"+data, clients);
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
        Broadcast(clients[clients.Count - 1].clientName + " has connected", clients);
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

public class ServerClient {
    public TcpClient tcp;
    public string clientName;

    public ServerClient(TcpClient clientSocket) {
        clientName = "Guest";
        tcp = clientSocket;
    }
}
