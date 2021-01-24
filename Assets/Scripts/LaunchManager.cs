using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    public GameObject EnterGamePanel;
    public GameObject ConnectionStatusPanel;
    public GameObject LobbyPanel;


    #region UNITY methods

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }



    // Start is called before the first frame update
    void Start()
    {
        EnterGamePanel.SetActive(true);                                     //At first enter game panel is active and Connection status panel is inactive
        ConnectionStatusPanel.SetActive(false);
        LobbyPanel.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Public Methods

    public void ConnectToPhotonServer()
    {
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();                   
            ConnectionStatusPanel.SetActive(true);                           //when button is clicked.....enter game panel is inactive and connection status panel is active
            EnterGamePanel.SetActive(false);
        }

        
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();       //if no room then callback

    }

    #endregion

    #region Photon Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log( PhotonNetwork.NickName + " Connected to photon server.");     //player name is connected to photon server
        LobbyPanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);


    }

    public override void OnConnected()    //when connected to internet
    {
        Debug.Log("Connected to internet.");
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);       //after joining room we can see who joined which room
        PhotonNetwork.LoadLevel("GameScene");        //load level GameScene as the room lobby

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " Joined to " + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);      //gives us which player joined the room and the no. of players in the room now


    }

    #endregion



    #region Private Methods

    void CreateAndJoinRoom()
    {
        string randomRoomName = "Room " + Random.Range(0, 10000);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 20;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }







    #endregion
}
