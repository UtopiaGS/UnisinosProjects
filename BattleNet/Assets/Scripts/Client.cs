using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using UnityEngine.UI;
using System;

public class Client : MonoBehaviour
{
   // public GameObject ChatContainer;
    //public GameObject MessagePrefab;
    private bool _socketReady;
    private TcpClient _socket;
    private NetworkStream _stream;
    private StreamWriter _writer;
    private StreamReader _reader;

    public InputField HostInput;
    public InputField PortInput;

    public Text PlayerName;
    // public InputField MessageField;
    public InputField NameField;

    public GameObject LoginPanel;

    public PlayerCharacters Player;

    public string ClientName;

    public void OnConnectedToServer()
    {
        ClientName = NameField.text;
        Debug.Log("NAME>>> " + ClientName);
        if (string.IsNullOrEmpty(ClientName)) ClientName = "Guest";
        PlayerName.text = ClientName;
        LoginPanel.SetActive(false);
        //if already connected, ignore this function
        if (_socketReady)
            return;

        //default host / port values
        string host = "127.0.0.1";
        int port;
        try
        {
            int.TryParse(PortInput.text, out port);
        }
        catch (Exception e) {
            Debug.Log(e);
            port = 41222;
        }

        if (!string.IsNullOrEmpty(HostInput.text)) {
            host = HostInput.text;
        }

        if (!string.IsNullOrEmpty(PortInput.text))
        {
            port = Int32.Parse(PortInput.text);
        }
        else {
            port = 41222;
        }

        //Create the socket
        Debug.Log(port);

        try {
            _socket = new TcpClient(host, port);
           
            _stream = _socket.GetStream();
            _writer = new StreamWriter(_stream);
            _reader = new StreamReader(_stream);
            _socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e.Message);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_socketReady)
        {

            if (_stream.DataAvailable) {
                string data = _reader.ReadLine();
                if (data != null)
                    OnIncomingData(data);
            }
        }
    }

    private void OnIncomingData(string data) {

        /*
        if (data=="%NAME") {
            Debug.Log("CLIENT NAME >> " + ClientName);
            Send("&NAME|" + ClientName);
            return;
        }
        if (data=="%STARTGAME|0") {
            Debug.Log("START GAME CHANGE CALLL>> " + data);
            Send("&STARTGAME");
            TurnsController.instance.ChangeTurn(0);
            return;
        }
        if (data.Contains("%END_TURN|0")) {
            Debug.Log("TERMINA TURNOOOOOO::::");
            return;
        }
        
        Debug.Log(data);
        */
        CommandReader.instance.ClientReadCommand(this, data);
        // Debug.Log("Server: " + data);
        //GameObject go = Instantiate(MessagePrefab, ChatContainer.transform);
        // go.GetComponentInChildren<Text>().text = data;
    }

    public void Send(string data) {
        if (!_socketReady)
            return;

        _writer.WriteLine(data);
        _writer.Flush();
    }

    public void OnSendButton(string text) {
        string message = text;
        Send(message);
    }

    private void CloseSocket() {
        if (!_socketReady) {
            return;
        }
        _writer.Close();
        _reader.Close();
        _socket.Close();
        _socketReady = false;
    }

    private void OnApplicationQuit()
    {
        CloseSocket(); 
    }

    private void OnDisable()
    {
        CloseSocket();
    }
}
