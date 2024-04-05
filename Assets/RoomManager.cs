using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject player;

    [Space]
    public Transform spawnPoint;

    [Space]
    public GameObject roomCam;

    public string roomNameToJoin = "test";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting...");

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to Server");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        Debug.Log("We are in the lobby.");

        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);
    }

    //THIS FUNCTION WE TIE TO THE FINAL "NEXT" BUTTON IN THE CREATE ROOM SCREEN (WE THEN GET RID OF TOP TWO FUNCTIONS)
    public void JoinRoomButtonPressed()
    {
        Debug.Log("Connecting...");

        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("We are connected and in a room now.");

        roomCam.SetActive(false);

        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);

        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
    }
}
