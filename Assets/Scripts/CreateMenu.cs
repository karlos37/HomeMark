using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using TMPro;
using static Room;

public class CreateMenu : MonoBehaviour
{

	private Room room;
	private GameObject placeholder;
	private TMP_InputField inputField;

	public GameObject MainMenu;
	public GameObject RoomManager;
	public RoomList roomList;
	public RoomManager roomManager;

	public GameObject movieListPanel;

	// Start is called before the first frame update
	void Start()
	{
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);

		room = new Room(true);

		placeholder = transform.Find("Canvas").Find("CreatePanel").Find("RoomInput").GetChild(0).Find("Placeholder").gameObject;
		inputField = transform.Find("Canvas").Find("CreatePanel").Find("RoomInput").gameObject.GetComponent<TMP_InputField>();
	}

	public void handleNextButton()
	{
		if (placeholder.GetComponent<TextMeshProUGUI>().text == "Room Name")
		{
			RoomNameOptions(true);
		}
		else if (placeholder.GetComponent<TextMeshProUGUI>().text == "Room Password (leave empty if public)")
		{
			RoomPasswordOptions(true);
		}
		else if (placeholder.GetComponent<TextMeshProUGUI>().text == "General Room Lighting (0-100, Default: 50)")
		{
			RoomLightingOptions(true);
		}
		else if (placeholder.GetComponent<TextMeshProUGUI>().text == "General Room Volume (0-100, Default: 50)")
		{
			RoomVolumeOptions(true);
		}
		else if (placeholder.GetComponent<TextMeshProUGUI>().text == "Room Background (default, mountains, night)")
		{
			RoomBackgroundOptions(true);
		}
		else if (placeholder.GetComponent<TextMeshProUGUI>().text == "Select Movie (default, demo)")
		{
			RoomMovieOptions(true);
		}
	}

	public void handleBackButton()
	{
		if (placeholder.GetComponent<TextMeshProUGUI>().text == "Room Name")
		{
			RoomNameOptions(false);
		}
		else if (placeholder.GetComponent<TextMeshProUGUI>().text == "Room Password (leave empty if public)")
		{
			RoomPasswordOptions(false);
		}
		else if (placeholder.GetComponent<TextMeshProUGUI>().text == "General Room Lighting (0-100, Default: 50)")
		{
			RoomLightingOptions(false);
		}
		else if (placeholder.GetComponent<TextMeshProUGUI>().text == "General Room Volume (0-100, Default: 50)")
		{
			RoomVolumeOptions(false);
		}
		else if (placeholder.GetComponent<TextMeshProUGUI>().text == "Room Background (default, mountains, night)")
		{
			RoomBackgroundOptions(false);
		}
		else if (placeholder.GetComponent<TextMeshProUGUI>().text == "Select Movie (default, demo)")
		{
			RoomMovieOptions(false);
		}
	}

	void RoomNameOptions(bool next)
	{
		if (next)
		{
			room.name = inputField.text;
			placeholder.GetComponent<TextMeshProUGUI>().text = "Room Password (leave empty if public)";
			inputField.text = "";

			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);
		}
		else
		{
			MainMenu.SetActive(true);
			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(MainMenu.transform.Find("Canvas").Find("IntroPanel").Find("Create Room Button").gameObject);
			gameObject.SetActive(false);
		}
	}

	void RoomPasswordOptions(bool next)
	{
		if (next)
		{
			room.password = inputField.text;
			if (String.IsNullOrEmpty(room.password))
			{
				room.isPublic = true;
			}
			placeholder.GetComponent<TextMeshProUGUI>().text = "General Room Lighting (0-100, Default: 50)";
		}
		else
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "Room Name";
		}
		inputField.text = "";
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);
	}

	void RoomLightingOptions(bool next)
	{
		if (next)
		{
			if (!String.IsNullOrEmpty(inputField.text))
			{
				room.lighting = float.Parse(inputField.text);
			}
			placeholder.GetComponent<TextMeshProUGUI>().text = "General Room Volume (0-100, Default: 50)";
		}
		else
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "Room Password (leave empty if public)";
		}
		inputField.text = "";
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);
	}

	void RoomVolumeOptions(bool next)
	{
		if (next)
		{
			if (!String.IsNullOrEmpty(inputField.text))
			{
				room.volume = float.Parse(inputField.text);
			}
			placeholder.GetComponent<TextMeshProUGUI>().text = "Room Background (default, mountains, night)";
		}
		else
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "General Room Lighting (0-100, Default: 50)";
		}
		inputField.text = "";
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);
	}

	void RoomBackgroundOptions(bool next)
	{
		if (next)
		{
			room.background = inputField.text;
			placeholder.GetComponent<TextMeshProUGUI>().text = "Select Movie (default, demo)";
		}
		else
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "General Room Volume (0-100, Default: 50)";
		}
		inputField.text = "";
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);
	}

	void RoomMovieOptions(bool next)
	{
		if (next)
		{
			room.movie = inputField.text;

			//CREATES THE ROOM
			CreateRoom();
			gameObject.SetActive(false); //hides the create menu
		}
		else
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "Room Background (default, mountains, night)";
			inputField.text = "";
			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);
		}
	}

	void CreateRoom()
	{
        //ENABLE THE ROOM MANAGER
        RoomManager.SetActive(true);

        //CREATE THE ROOM BY SENDING THE NAME, YOU CAN SEND OTHER THINGS HERE TOO, BUT YOU MUST CREATE RECEIVING FUNCTIONS IN OTHER SCRIPTS TOO
        roomList.ChangeRoomToCreateName(room.name);
		roomManager.room = room;
        roomManager.JoinRoomButtonPressed();
    }
}
