using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    public GameObject player;
    public Transform spawnPoint;
    public GameObject roomCam;
    public GameObject menuCharacter;
    public string roomNameToJoin = "test";
    public VideoPlayer videoPlayer;
    public Material spaceSkyBoxMaterial;
    public Material mountainSkyboxMaterial;
    public GameObject theatre;
    public GameObject mainFloor;
    
    public Room room;

    private List<MeshRenderer> _wallsMeshRenderers;
    private GameObject _seats;
    private GameObject _levels;
    private GameObject _stairs;
    
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
        RoomOptions roomOptions = new RoomOptions();
        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable();
        customRoomProperties["movie_url"] = room.movie;
        customRoomProperties["background"] = room.background;
        roomOptions.CustomRoomProperties = customRoomProperties;
        // Check if PhotonNetwork is connected to the master server before joining or creating a room
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, roomOptions, null);
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
        Room.Background bg  = (Room.Background)PhotonNetwork.CurrentRoom.CustomProperties["background"];
        ChangeBackgroundSettings(bg,theatre, _player);
    }
    
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        string movie_url = (string)PhotonNetwork.CurrentRoom.CustomProperties["movie_url"];
        videoPlayer.url = movie_url;
        MeshRenderer[] meshRenderers = theatre.transform.Find("Walls").GetComponentsInChildren<MeshRenderer>();
        _wallsMeshRenderers = new List<MeshRenderer>(meshRenderers);
        _seats = theatre.transform.Find("Seats").gameObject;
        _stairs = theatre.transform.Find("Stairs").gameObject;
        _levels = theatre.transform.Find("Levels").gameObject;
    }

    // Handle case when joining room fails
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to join room: " + message);
    }

    private void ChangeBackgroundSettings(Room.Background bg, GameObject theater1, GameObject _player)
    {
        GameObject cameraObj =
            _player.transform.GetChild(0).Find("MainCamera").gameObject;
        switch (bg)
        {
            case Room.Background.Mountain:
                print("SnowMountain Background Selected For room");
                AddSkyBoxAndHideFloor(cameraObj, mainFloor, theater1, mountainSkyboxMaterial);
                GameObject mountain = theater1.transform.Find("Plane").Find("Snow_mountain").gameObject;
                mountain.SetActive(true);
                break;
            case Room.Background.Space:
                print("Space Backround Selected for room");
                AddSkyBoxAndHideFloor(cameraObj, mainFloor, theater1,spaceSkyBoxMaterial);
                GameObject ship1 = theater1.transform.Find("Plane").Find("Example1_Red").gameObject;
                GameObject ship2 = theater1.transform.Find("Plane").Find("Example2_Grey").gameObject;
                GameObject ship3 = theater1.transform.Find("Plane").Find("Example5_Grey").gameObject;
                ship1.SetActive(true);
                ship2.SetActive(true);
                ship3.SetActive(true);
                break;
            default:
                print("Theatre background selected for room");
                break;
        }
    }

    private void AddSkyBoxAndHideFloor(GameObject cameraObj, GameObject floor, GameObject theater1, Material skyBoxMaterial)
    {
        Camera playerCam = cameraObj.GetComponent<Camera>();
        Skybox skyboxComponent = cameraObj.AddComponent<Skybox>();
        skyboxComponent.material = skyBoxMaterial;
        playerCam.clearFlags = CameraClearFlags.Skybox;
        // foreach (MeshRenderer wallRenderer in _wallsMeshRenderers)
        // {
        //     wallRenderer.enabled = false;
        // }
        _seats.SetActive(false);
        _stairs.SetActive(false);
        _levels.SetActive(false);
        MeshRenderer floorMeshRenderer = floor.GetComponent<MeshRenderer>();
        floorMeshRenderer.enabled = false;

        MeshRenderer theatreFloor = theater1.transform.Find("Plane").GetComponent<MeshRenderer>();
        theatreFloor.enabled = false;
    }
}
