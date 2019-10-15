using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using UnityEngine.UI;
using System;

public class Client : MonoBehaviour
{
    public GameObject ChatContainer;
    public GameObject MessagePrefab;
    private bool _socketReady;
    private TcpClient _socket;
    private NetworkStream _stream;
    private StreamWriter _writer;
    private StreamReader _reader;

    public InputField HostInput;
    public InputField PortInput;
    public InputField MessageField;

    public string ClientName;

    public void OnConnectedToServer()
    {
        //if already connected, ignore this function
        if (_socketReady)
            return;

        //default host / port values
        string host = "127.0.0.1";
        int port = 6321;

        if (!string.IsNullOrEmpty(HostInput.text)) {
            host = HostInput.text;
        }

        if (!string.IsNullOrEmpty(PortInput.text))
        {
            port = Int32.Parse(PortInput.text);
        }

        //Create the socket

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
        // Debug.Log("Server: " + data);
       GameObject go = Instantiate(MessagePrefab, ChatContainer.transform);
        go.GetComponentInChildren<Text>().text = data;
    }

    private void Send(string data) {
        if (!_socketReady)
            return;

        _writer.WriteLine(data);
        _writer.Flush();
    }

    public void OnSendButton() {
        string message = MessageField.text;
        Send(message);
    }
}
