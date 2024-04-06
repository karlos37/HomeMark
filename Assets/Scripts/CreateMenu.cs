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
	[SerializeField] private XRCardboardInputModule vrInputModule;
	[SerializeField] private StandaloneInputModuleCopy standardInputModuleCopy;

	[SerializeField] private Transform createMenuTransform;
	[SerializeField] private GameObject eventSystemObject;

	private string roomName;
	private string roomPassword;
	private float roomLighting;
	private float roomVolume;
	private string roomBackground;
	private string roomMovie;

	private GameObject placeholder;
	private TMP_InputField inputField;

	public GameObject GameMenu;
	public GameObject RoomManager;
	public RoomList roomList;
	public RoomManager roomManager;

	// Start is called before the first frame update
	void Start()
	{
		eventSystemObject.SetActive(false);
		eventSystemObject.SetActive(true);

		standardInputModuleCopy.enabled = true;
		vrInputModule.enabled = false;

		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("RoomInput").gameObject);

		roomName = "";
		roomPassword = "";
		roomLighting = 0;
		roomVolume = 0;
		roomBackground = "";
		roomMovie = "";

		placeholder = createMenuTransform.Find("RoomInput").GetChild(0).Find("Placeholder").gameObject;
		inputField = createMenuTransform.Find("RoomInput").gameObject.GetComponent<TMP_InputField>();
	}

	// Update is called once per frame
	void Update()
	{
		if (vrInputModule.enabled)
		{
			eventSystemObject.SetActive(false);
			eventSystemObject.SetActive(true);

			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("RoomInput").gameObject);

			standardInputModuleCopy.enabled = true;
			vrInputModule.enabled = false;
		}
		NavigateMenu();
	}

	void NavigateMenu()
	{
		if (Input.GetButtonDown("js1"))
		{
			if (placeholder.GetComponent<TextMeshProUGUI>().text == "Room Name")
			{
				RoomNameOptions();
			}
			else if (placeholder.GetComponent<TextMeshProUGUI>().text == "Room Password (leave empty if public)")
			{
				RoomPasswordOptions();
			}
			else if (placeholder.GetComponent<TextMeshProUGUI>().text == "General Room Lighting (0-100, Default: 50)")
			{
				RoomLightingOptions();
			}
			else if (placeholder.GetComponent<TextMeshProUGUI>().text == "General Room Volume (0-100, Default: 50)")
			{
				RoomVolumeOptions();
			}
			else if (placeholder.GetComponent<TextMeshProUGUI>().text == "Room Background (default, mountains, or night)")
			{
				RoomBackgroundOptions();
			}
			else if (placeholder.GetComponent<TextMeshProUGUI>().text == "Select Movie: Random text for now")
			{
				RoomMovieOptions();
			}
		}
	}

	void RoomNameOptions()
	{
		if (EventSystem.current.currentSelectedGameObject.name == "NextButton")
		{
			roomName = inputField.text;
			placeholder.GetComponent<TextMeshProUGUI>().text = "Room Password (leave empty if public)";
			inputField.text = "";

			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("RoomInput").gameObject);
		}
		else if (EventSystem.current.currentSelectedGameObject.name == "BackButton")
		{
			//SceneManager.LoadScene("MainMenu");
		}
	}

	void RoomPasswordOptions()
	{
		if (EventSystem.current.currentSelectedGameObject.name == "NextButton")
		{
			roomPassword = inputField.text;
			//if (String.IsNullOrEmpty(roomPassword))
			//{
			//		Room.isPublic = true;
			//}
			placeholder.GetComponent<TextMeshProUGUI>().text = "General Room Lighting (0-100, Default: 50)";
		}
		else if (EventSystem.current.currentSelectedGameObject.name == "BackButton")
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "Room Name";
		}
		inputField.text = "";
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("RoomInput").gameObject);
	}

	void RoomLightingOptions()
	{
		if (EventSystem.current.currentSelectedGameObject.name == "NextButton")
		{
			if (String.IsNullOrEmpty(inputField.text))
			{
				roomLighting = 50f;
			}
			else
			{
				Debug.Log(inputField.text);
				roomLighting = float.Parse(inputField.text);
			}
			placeholder.GetComponent<TextMeshProUGUI>().text = "General Room Volume (0-100, Default: 50)";
		}
		else if (EventSystem.current.currentSelectedGameObject.name == "BackButton")
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "Room Password (leave empty if public)";
		}
		inputField.text = "";
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("RoomInput").gameObject);
	}

	void RoomVolumeOptions()
	{
		if (EventSystem.current.currentSelectedGameObject.name == "NextButton")
		{
			if (String.IsNullOrEmpty(inputField.text))
			{
				roomVolume = 50f;
			}
			else
			{
				roomVolume = float.Parse(inputField.text);
			}
			placeholder.GetComponent<TextMeshProUGUI>().text = "Room Background (default, mountains, or night)";
		}
		else if (EventSystem.current.currentSelectedGameObject.name == "BackButton")
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "General Room Lighting (0-100, Default: 50)";
		}
		inputField.text = "";
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("RoomInput").gameObject);
	}

	void RoomBackgroundOptions()
	{
		if (EventSystem.current.currentSelectedGameObject.name == "NextButton")
		{
			roomBackground = inputField.text;
			placeholder.GetComponent<TextMeshProUGUI>().text = "Select Movie: Random text for now";
		}
		else if (EventSystem.current.currentSelectedGameObject.name == "BackButton")
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "General Room Volume (0-100, Default: 50)";
		}
		inputField.text = "";
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("RoomInput").gameObject);
	}

	void RoomMovieOptions()
	{
		if (EventSystem.current.currentSelectedGameObject.name == "NextButton")
		{
			roomMovie = inputField.text;
			//Create/Store Room
			//SceneManager.LoadScene("Theater");

			//CREATES THE ROOM
			CreateRoom();
			gameObject.SetActive(false); //hides the create menu
		}
		else if (EventSystem.current.currentSelectedGameObject.name == "BackButton")
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "Select Movie: Random text for now";
			inputField.text = "";
			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("RoomInput").gameObject);
		}
	}

	void CreateRoom()
	{
        //ENABLE THE ROOM MANAGER
        RoomManager.SetActive(true);

        //CREATE THE ROOM BY SENDING THE NAME, YOU CAN SEND OTHER THINGS HERE TOO, BUT YOU MUST CREATE RECEIVING FUNCTIONS IN OTHER SCRIPTS TOO
        roomList.ChangeRoomToCreateName(roomName);
        roomManager.JoinRoomButtonPressed();
    }
}