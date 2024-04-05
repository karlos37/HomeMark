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

	// Start is called before the first frame update
	void Start()
	{
		eventSystemObject.SetActive(false);
		eventSystemObject.SetActive(true);

		standardInputModuleCopy.enabled = true;
		vrInputModule.enabled = false;

		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("NextButton").gameObject);

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
		else if (placeholder.GetComponent<TextMeshProUGUI>().text == "Room Background (default, mountains, or night)")
		{
			RoomBackgroundOptions(true);
		}
		else if (placeholder.GetComponent<TextMeshProUGUI>().text == "Select Movie: Random text for now")
		{
			RoomMovieOptions(true);
		}
	}

	public void handleBacktButton()
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
		else if (placeholder.GetComponent<TextMeshProUGUI>().text == "Room Background (default, mountains, or night)")
		{
			RoomBackgroundOptions(false);
		}
		else if (placeholder.GetComponent<TextMeshProUGUI>().text == "Select Movie: Random text for now")
		{
			RoomMovieOptions(false);
		}
	}

	void RoomNameOptions(bool next)
	{
		if (next)
		{
			roomName = inputField.text;
			placeholder.GetComponent<TextMeshProUGUI>().text = "Room Password (leave empty if public)";
			inputField.text = "";

			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("NextButton").gameObject);
		}
		else
		{
			SceneManager.LoadScene("MainMenu");
		}
	}

	void RoomPasswordOptions(bool next)
	{
		if (next)
		{
			roomPassword = inputField.text;
			//if (String.IsNullOrEmpty(roomPassword))
			//{
			//		Room.isPublic = true;
			//}
			placeholder.GetComponent<TextMeshProUGUI>().text = "General Room Lighting (0-100, Default: 50)";
		}
		else
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "Room Name";
		}
		inputField.text = "";
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("NextButton").gameObject);
	}

	void RoomLightingOptions(bool next)
	{
		if (next)
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
		else
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "Room Password (leave empty if public)";
		}
		inputField.text = "";
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("NextButton").gameObject);
	}

	void RoomVolumeOptions(bool next)
	{
		if (next)
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
		else
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "General Room Lighting (0-100, Default: 50)";
		}
		inputField.text = "";
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("NextButton").gameObject);
	}

	void RoomBackgroundOptions(bool next)
	{
		if (next)
		{
			roomBackground = inputField.text;
			placeholder.GetComponent<TextMeshProUGUI>().text = "Select Movie: Random text for now";
		}
		else
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "General Room Volume (0-100, Default: 50)";
		}
		inputField.text = "";
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("NextButton").gameObject);
	}

	void RoomMovieOptions(bool next)
	{
		if (next)
		{
			roomMovie = inputField.text;
			//Create/Store Room
			SceneManager.LoadScene("Theater");
		}
		else
		{
			placeholder.GetComponent<TextMeshProUGUI>().text = "Select Movie: Random text for now";
			inputField.text = "";
			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(createMenuTransform.Find("NextButton").gameObject);
		}
	}
}
