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
	private GameObject inputText;

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
		inputText = createMenuTransform.Find("RoomInput").GetChild(0).Find("Text").gameObject;
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
		if (Input.GetKeyDown("m"))
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
			roomName = inputText.GetComponent<TextMeshProUGUI>().text;
			placeholder.GetComponent<TextMeshProUGUI>().text = "Room Password (leave empty if public)";
			inputField.text = "";
			inputText.GetComponent<TextMeshProUGUI>().text = "";

			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("RoomInput").gameObject);
		}
		else if (EventSystem.current.currentSelectedGameObject.name == "BackButton")
		{
			SceneManager.LoadScene("MainMenu");
		}
	}

	void RoomPasswordOptions()
	{
		if (EventSystem.current.currentSelectedGameObject.name == "NextButton")
		{
			roomPassword = inputText.GetComponent<TextMeshProUGUI>().text;
			placeholder.GetComponent<TextMeshProUGUI>().text = "General Room Lighting (0-100, Default: 50)";
		}
		else if (EventSystem.current.currentSelectedGameObject.name == "BackButton")
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "Room Name";
		}
		inputField.text = "";
		inputText.GetComponent<TextMeshProUGUI>().text = "";
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("RoomInput").gameObject);
	}

	void RoomLightingOptions()
	{
		if (EventSystem.current.currentSelectedGameObject.name == "NextButton")
		{
			if (inputText.GetComponent<TextMeshProUGUI>().text == "")
			{
				roomLighting = 50f;
			}
			else
			{
				roomLighting = float.Parse(inputText.GetComponent<TextMeshProUGUI>().text);
			}
			placeholder.GetComponent<TextMeshProUGUI>().text = "General Room Volume (0-100, Default: 50)";
		}
		else if (EventSystem.current.currentSelectedGameObject.name == "BackButton")
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "Room Password (leave empty if public)";
		}
		inputField.text = "";
		inputText.GetComponent<TextMeshProUGUI>().text = "";
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("RoomInput").gameObject);
	}

	void RoomVolumeOptions()
	{
		if (EventSystem.current.currentSelectedGameObject.name == "NextButton")
		{
			if (inputText.GetComponent<TextMeshProUGUI>().text == "")
			{
				roomVolume = 50f;
			}
			else
			{
				roomVolume = float.Parse(inputText.GetComponent<TextMeshProUGUI>().text);
			}
			placeholder.GetComponent<TextMeshProUGUI>().text = "Room Background (default, mountains, or night)";
		}
		else if (EventSystem.current.currentSelectedGameObject.name == "BackButton")
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "General Room Lighting (0-100, Default: 50)";
		}
		inputField.text = "";
		inputText.GetComponent<TextMeshProUGUI>().text = "";
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("RoomInput").gameObject);
	}

	void RoomBackgroundOptions()
	{
		if (EventSystem.current.currentSelectedGameObject.name == "NextButton")
		{
			roomBackground = inputText.GetComponent<TextMeshProUGUI>().text;
			placeholder.GetComponent<TextMeshProUGUI>().text = "Select Movie: Random text for now";
		}
		else if (EventSystem.current.currentSelectedGameObject.name == "BackButton")
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "General Room Volume (0-100, Default: 50)";
		}
		inputField.text = "";
		inputText.GetComponent<TextMeshProUGUI>().text = "";
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("RoomInput").gameObject);
	}

	void RoomMovieOptions()
	{
		if (EventSystem.current.currentSelectedGameObject.name == "NextButton")
		{
			roomMovie = inputText.GetComponent<TextMeshProUGUI>().text;
			//Create/Store Room
			//SceneManager.LoadScene("Theater");
		}
		else if (EventSystem.current.currentSelectedGameObject.name == "BackButton")
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "Select Movie: Random text for now";
			inputField.text = "";
			inputText.GetComponent<TextMeshProUGUI>().text = "";
			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("RoomInput").gameObject);
		}
	}
}
