using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using TMPro;

public class CreateMenu : MonoBehaviour
{

	private Room room;
	private GameObject placeholder;
	private TMP_InputField inputField;
	private GameObject _keyboardObject;
	private GameObject _inputGameObject;
	
	public GameObject MainMenu;
	public GameObject RoomManager;
	public RoomList roomList;
	public RoomManager roomManager;
	public GameObject movieListPanel;
	public GameObject nextButton;

	private const string ROOM_NAME_PLACEHOLDER = "Room Name";
	private const string ROOM_PASSWORD_PLACEHOLDER = "Room Password (leave empty if public)";
	private const string ROOM_LIGHTING_PLACEHOLDER = "General Room Lighting (0-100, Default: 50)";
	private const string ROOM_VOLUME_PLACEHOLDER = "General Room Volume (0-100, Default: 50)";
	private const string ROOM_BACKGROUND_PLACEHOLDER = "Room Background (default, mountains, night)";
	private const string ROOM_MOVIE_PLACEHOLDER = "Select Movie (default, demo)";

	// Start is called before the first frame update
	void Start()
	{
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);

		room = new Room(true);

		placeholder = transform.Find("Canvas").Find("CreatePanel").Find("RoomInput").GetChild(0).Find("Placeholder").gameObject;
		_inputGameObject = transform.Find("Canvas").Find("CreatePanel").Find("RoomInput").gameObject;
		inputField = _inputGameObject.GetComponent<TMP_InputField>();
		_keyboardObject = transform.Find("Canvas").Find("OnScreenKeyboard1").gameObject;
	}

	public void handleNextButton()
	{
		switch(placeholder.GetComponent<TextMeshProUGUI>().text) {

			case ROOM_NAME_PLACEHOLDER:
				RoomNameOptions(true);
				break;
			case ROOM_PASSWORD_PLACEHOLDER:
				RoomPasswordOptions(true);
				break;
			case ROOM_LIGHTING_PLACEHOLDER:
				RoomLightingOptions(true);
				break;
			case ROOM_VOLUME_PLACEHOLDER:
				RoomVolumeOptions(true);
				break;
			case ROOM_BACKGROUND_PLACEHOLDER:
				RoomBackgroundOptions(true);
				break;
			case ROOM_MOVIE_PLACEHOLDER:
				RoomMovieOptions(true);
				break;
		}
	}

	public void handleBackButton()
	{
		switch (placeholder.GetComponent<TextMeshProUGUI>().text) {

			case ROOM_NAME_PLACEHOLDER:
				RoomNameOptions(false);
				break;
			case ROOM_PASSWORD_PLACEHOLDER:
				RoomPasswordOptions(false);
				break;
			case ROOM_LIGHTING_PLACEHOLDER:
				RoomLightingOptions(false);
				break;
			case ROOM_VOLUME_PLACEHOLDER:
				RoomVolumeOptions(false);
				break;
			case ROOM_BACKGROUND_PLACEHOLDER:
				RoomBackgroundOptions(false);
				break;
			case ROOM_MOVIE_PLACEHOLDER:
				RoomMovieOptions(false);
				break;
		}
	}

	void RoomNameOptions(bool next)
	{
		if (next)
		{
			room.name = inputField.text;
			placeholder.GetComponent<TextMeshProUGUI>().text = ROOM_PASSWORD_PLACEHOLDER;
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
			if (string.IsNullOrEmpty(room.password))
			{
				room.isPublic = true;
			}
			placeholder.GetComponent<TextMeshProUGUI>().text = ROOM_LIGHTING_PLACEHOLDER;
			inputField.text = "";
			
		}
		else
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = ROOM_NAME_PLACEHOLDER;
			if (string.IsNullOrEmpty(room.name))
            {
				inputField.text = "";
			}
			else
            {
				inputField.text = room.name;
            }
			
		}
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);
	}

	void RoomLightingOptions(bool next)
	{
		if (next)
		{
			if (!string.IsNullOrEmpty(inputField.text))
			{
				room.lighting = float.Parse(inputField.text);
			}
			placeholder.GetComponent<TextMeshProUGUI>().text = ROOM_VOLUME_PLACEHOLDER;
			inputField.text = room.volume.ToString();
		}
		else
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = ROOM_PASSWORD_PLACEHOLDER;
			if (string.IsNullOrEmpty(room.password))
            {
				inputField.text = "";
            }
			else
            {
				inputField.text = room.password;
            }
		}
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);
	}

	void RoomVolumeOptions(bool next)
	{
		if (next)
		{
			if (!string.IsNullOrEmpty(inputField.text))
			{
				room.volume = float.Parse(inputField.text);
			}
			inputField.text = "";
			placeholder.GetComponent<TextMeshProUGUI>().text = ROOM_BACKGROUND_PLACEHOLDER;
		}
		else
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = ROOM_LIGHTING_PLACEHOLDER;
			inputField.text = "";
		}
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);
	}

	void RoomBackgroundOptions(bool next)
	{
		if (next)
		{
			movieListPanel.SetActive(true);
			nextButton.SetActive(false);
			room.background = inputField.text;
			_keyboardObject.SetActive(false);
			_inputGameObject.SetActive(false);
			// placeholder.GetComponent<TextMeshProUGUI>().text = ROOM_MOVIE_PLACEHOLDER;
		}
		else
		{
			movieListPanel.SetActive(false);
			nextButton.SetActive(true);
			_inputGameObject.SetActive(true);
			_keyboardObject.SetActive(true);
			placeholder.GetComponent<TextMeshProUGUI>().text = ROOM_VOLUME_PLACEHOLDER;
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
			placeholder.GetComponent<TextMeshProUGUI>().text = ROOM_BACKGROUND_PLACEHOLDER;
			inputField.text = "";
			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);
		}
	}

	public void OnMovieSelected(string moviePath)
	{
		print(moviePath);
		Debug.Log(moviePath);
		room.movie = moviePath;
		movieListPanel.SetActive(false);
		gameObject.SetActive(false);
		CreateRoom();
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
