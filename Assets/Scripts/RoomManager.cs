using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    public GameObject player;
    public Transform spawnPoint;
    public GameObject roomCam;
    public GameObject menuCharacter;
    public string roomNameToJoin = "test";

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Ensure PhotonNetwork is properly initialized
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void JoinRoomButtonPressed()
    {
        Debug.Log("Connecting...");
        // Check if PhotonNetwork is connected to the master server before joining or creating a room
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);
        }
        else
        {
            Debug.Log("PhotonNetwork is not connected to the master server.");
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server.");
        // After connecting to the master server, join or create the room
        JoinRoomButtonPressed();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room successfully.");
        // Disable room camera and menu character
        roomCam.SetActive(false);
        menuCharacter.SetActive(false);
        // Instantiate player at spawn point
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);

    }

    // Handle case when joining room fails
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to join room: " + message);
    }
}