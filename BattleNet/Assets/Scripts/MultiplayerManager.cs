using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerManager : MonoBehaviour
{
    public Server server;
    public Client HostClient;
    public Client Client;
    public Dropdown HostOption;
    public GameObject WaitForPlayersPanel;

    public void Connect()
    {
        if (HostOption.value == 0) {
            StartHostMatch();
        }
        if (HostOption.value == 1)
        {
            EnterMatch();           
        }
    }


    public void StartHostMatch() {
        server.StartServer();
        HostClient.OnConnectedToServer();
        TurnsController.instance.instancePlayer = HostClient.gameObject.GetComponent<PlayerCharacters>();
    }

    public void EnterMatch() {
        Client.OnConnectedToServer();
        WaitForPlayersPanel.SetActive(false);
        TurnsController.instance.instancePlayer = Client.gameObject.GetComponent<PlayerCharacters>();
        server.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
