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
	private GameObject _keyboardObject;
	private GameObject _inputGameObject;
	private GameObject _navigationGameObject;
	private GameObject _keyboardInstructionsGameObject;
	
	public GameObject MainMenu;
	public GameObject JoinScreen;
	public GameObject RoomManager;
	public RoomList roomList;
	public RoomManager roomManager;
	public GameObject movieListPanel;
	public GameObject backgroundListPane;
	public GameObject nextButton;

	private const string ROOM_NAME_PLACEHOLDER = "Enter Room Name";
	private const string ROOM_USER_PLACEHOLDER = "Enter User Name";
	private const string NAME = "name";
	private const string USER = "user";
	private const string USERJOIN = "userJoin";
	private const string BACKGROUND = "background";
	private const string MOVIE = "movie";

	private string current_screen;
	private string player_name;

	// Start is called before the first frame update
	void Start()
	{
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);

		room = new Room(true);
		player_name = "";

		placeholder = transform.Find("Canvas").Find("CreatePanel").Find("RoomInput").GetChild(0).Find("Placeholder").gameObject;
		_inputGameObject = transform.Find("Canvas").Find("CreatePanel").Find("RoomInput").gameObject;
		inputField = _inputGameObject.GetComponent<TMP_InputField>();
		_keyboardObject = transform.Find("Canvas").Find("OnScreenKeyboard1").gameObject;
		_navigationGameObject = transform.Find("Canvas").Find("CreatePanel").Find("Instruction").gameObject;
		_keyboardInstructionsGameObject = transform.Find("Canvas").Find("CreatePanel").Find("Keyboard").gameObject;
	}

	public void HandleNextButton()
	{
		switch(current_screen) 
		{
			case NAME:
				RoomNameOptions(true);
				break;
			case USER:
				RoomUserOptions(true, true);
				break;
			case USERJOIN:
				RoomUserOptions(true, false);
				break;
			default:
				print("Not a valid option");
				break;
		}
	}

	public void HandleBackButton()
	{
		switch (current_screen) 
		{
			case NAME:
				RoomNameOptions(false);
				break;
			case USER:
				RoomUserOptions(false, true);
				break;
			case USERJOIN:
				RoomUserOptions(false, false);
				break;
			case BACKGROUND:
				RoomBackgroundBackOptions();
				break;
			case MOVIE:
				RoomMovieBackOptions();
				break;
		}
	}

	void RoomNameOptions(bool next)
	{
		if (next)
		{
			room.name = inputField.text;
			inputField.text = "";
			placeholder.GetComponent<TextMeshProUGUI>().text = ROOM_USER_PLACEHOLDER;
			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);
			current_screen = USER;
		}
		else
		{
			MainMenu.SetActive(true);
			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(MainMenu.transform.Find("Canvas").Find("IntroPanel").Find("Create Room Button").gameObject);
			gameObject.SetActive(false);
		}
	}

	void RoomUserOptions(bool next, bool fromCreate)
	{
		if(fromCreate)
		{
			if (next)
			{
				player_name = inputField.text;

				backgroundListPane.SetActive(true);
				nextButton.SetActive(false);
				_keyboardObject.SetActive(false);
				_inputGameObject.SetActive(false);
				_navigationGameObject.SetActive(false);
				_keyboardInstructionsGameObject.SetActive(false);

				current_screen = BACKGROUND;
			}
			else
			{
				placeholder.GetComponent<TextMeshProUGUI>().text = ROOM_NAME_PLACEHOLDER;
				inputField.text = room.name;
				EventSystem.current.SetSelectedGameObject(null);
				EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);
				current_screen = NAME;
			}
		}
		else
		{
			if (next)
			{
				JoinScreen.SetActive(true);
				JoinScreen.GetComponent<RoomList>().SetPlayerName(player_name);
			}
			else
			{
				MainMenu.SetActive(true);
				EventSystem.current.SetSelectedGameObject(null);
				EventSystem.current.SetSelectedGameObject(MainMenu.transform.Find("Canvas").Find("IntroPanel").Find("Create Room Button").gameObject);
			}
			gameObject.SetActive(false);
		}
	
	}

	void RoomBackgroundBackOptions()
	{
		nextButton.SetActive(true);
		_inputGameObject.SetActive(true);
		_keyboardObject.SetActive(true);
		_navigationGameObject.SetActive(true);
		_keyboardInstructionsGameObject.SetActive(true);

		placeholder.GetComponent<TextMeshProUGUI>().text = ROOM_USER_PLACEHOLDER;
		inputField.text = player_name;
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(transform.Find("Canvas").Find("CreatePanel").Find("NextButton").gameObject);
		current_screen = USER;

		backgroundListPane.SetActive(false);
	}

	void RoomMovieBackOptions()
	{
		current_screen = BACKGROUND;
		movieListPanel.SetActive(false);
		backgroundListPane.SetActive(true);
	}
	
	public void OnBackgroundSelected(Background bg)
	{
		room.background = bg;
		current_screen = MOVIE;
		backgroundListPane.SetActive(false);
		movieListPanel.SetActive(true);
	}
	
	public void OnMovieSelected(string moviePath)
	{
		room.movie = moviePath;
		movieListPanel.SetActive(false);
		gameObject.SetActive(false);
		CreateRoom();
	}

	void CreateRoom()
	{
        //ENABLE THE ROOM MANAGER
        RoomManager.SetActive(true);
		roomManager.SetPlayerName(player_name);

		//CREATE THE ROOM BY SENDING THE NAME, YOU CAN SEND OTHER THINGS HERE TOO, BUT YOU MUST CREATE RECEIVING FUNCTIONS IN OTHER SCRIPTS TOO
		roomList.ChangeRoomToCreateName(room.name);
		roomManager.room = room;
		
		roomManager.CreateRoomButtonPressed();
    }

	public void SetScreen(string screen)
	{
		current_screen = screen;
	}

	public void SetPlaceholderText()
	{
		transform.Find("Canvas").Find("CreatePanel").Find("RoomInput").GetChild(0).Find("Placeholder").gameObject.GetComponent<TextMeshProUGUI>().text = ROOM_USER_PLACEHOLDER;
	}
}
