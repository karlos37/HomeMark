using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.EventSystems;

public class RoomList : MonoBehaviourPunCallbacks
{
    public static RoomList instance;

    public GameObject roomManagerGameobject;
    public RoomManager roomManager;

    [Header("UI")] public Transform roomListParent;
    public GameObject roomListItemPrefab;

    private List<RoomInfo> cachedRoomList = new List<RoomInfo>();
    private List<GameObject> roomObjList = new List<GameObject>();
    
    public string playerName;

	public void ChangeRoomToCreateName(string _roomName)
    {
        roomManager.roomNameToJoin = _roomName;
    }
    
    private void Awake()
    {
        instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        if (roomListParent.childCount > 1)
        {
            EventSystem.current.SetSelectedGameObject(roomListParent.GetChild(1).gameObject);
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }
    }

    IEnumerator Start()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }

        yield return new WaitUntil(() => !PhotonNetwork.IsConnected);

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (cachedRoomList.Count <= 0)
        {
            cachedRoomList = roomList;
        }
        else
        {
            foreach (var room in roomList)
            {
                for (int i = 0; i < cachedRoomList.Count; i++)
                {
                    if (cachedRoomList[i].Name == room.Name)
                    {
                        List<RoomInfo> newList = cachedRoomList;

                        if (room.RemovedFromList)
                        {
                            newList.Remove(newList[i]);
                        }
                        else
                        {
                            newList[i] = room;
                        }

                        cachedRoomList = newList;
                    }
                }
            }
        }

        UpdateUI();
    }


    void UpdateUI()
    {
        foreach (Transform roomItem in roomListParent)
        {
            Destroy(roomItem.gameObject);
        }

        roomObjList = new List<GameObject>();
        foreach (var room in cachedRoomList)
        {
            GameObject roomItem = Instantiate(roomListItemPrefab, roomListParent);

            roomItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name; 

            roomItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Movie Name";

            roomItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = room.PlayerCount + "/16"; //bc 16 is max room size in photon

            roomItem.GetComponent<RoomItemButton>().RoomName = room.Name; //Give the room name that we will try joining

			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(roomItem);
            roomObjList.Add(roomItem);
		}
    }

    public void JoinRoomByName(string _name)
    {
        roomManager.roomNameToJoin = _name;
        roomManagerGameobject.SetActive(true);
		roomManager.SetPlayerName(playerName);
		roomManager.JoinRoomButtonPressed(); //calls to join room with the room name we already set should technically do this but meh

        gameObject.SetActive(false);

    }

	public void SetPlayerName(string pName)
	{
		playerName = pName;
	}
}
