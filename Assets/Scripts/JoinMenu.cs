using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using TMPro;
using static Room;

public class JoinMenu : MonoBehaviour
{
	[SerializeField] private TMP_InputField search;

	[SerializeField] private XRCardboardInputModule vrInputModule;
	[SerializeField] private StandaloneInputModuleCopy standardInputModuleCopy;

	[SerializeField] private Transform joinMenuTransform;
	[SerializeField] private GameObject eventSystemObject;

	private string searchText;
	private List<Room> rooms;
	private List<Room> allRooms;

	// Start is called before the first frame update
	void Start()
    {
		searchText = "";

		allRooms = new List<Room>();
		allRooms.Add(new Room("Test Room 1", true, "Now Playing: Demo Part 1", 0.5f, 0.5f, "default"));
		allRooms.Add(new Room("testjkhskjsl", false, "password", "Pirates of the Caribbean Part 79", 0.5f, 0.5f, "default"));
		allRooms.Add(new Room("apsodur", true, "9821374slkjdf", 0.5f, 0.5f, "default"));
		allRooms.Add(new Room("djg8439jd", false, "password", "aisduasdas8d7", 0.5f, 0.5f, "default"));
		allRooms.Add(new Room("98^&3js^&", true, "2984734289347", 0.5f, 0.5f, "default"));
		allRooms.Add(new Room("sjkdfhchzl98", false, "password", " 8372498374238", 0.5f, 0.5f, "default"));
		allRooms.Add(new Room("ijjasjd8923jksd", true, "asdh83h4", 0.5f, 0.5f, "default"));
		allRooms.Add(new Room("387927838927398", false, "password", "superman", 0.5f, 0.5f, "default"));
		allRooms.Add(new Room("98dgss^&", true, "2984734289347", 0.5f, 0.5f, "default"));
		allRooms.Add(new Room("s3434hchzl98", false, "password", " 8372498374238", 0.5f, 0.5f, "default"));
		allRooms.Add(new Room("ijsdf38923jksd", true, "Pirates of the Caribbean Part 19", 0.5f, 0.5f, "default"));
		allRooms.Add(new Room("38sdfb./398", false, "password", "Pirates of the Caribbean Part 49", 0.5f, 0.5f, "default"));

		rooms = new List<Room>();
		rooms = allRooms.GetRange(0, Math.Min(5, allRooms.Count));
		SetupRooms();

		eventSystemObject.SetActive(false);
		eventSystemObject.SetActive(true);

		standardInputModuleCopy.enabled = true;
		vrInputModule.enabled = false;

		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(joinMenuTransform.Find(rooms[0].name).Find("Button1").gameObject);
	}

    // Update is called once per frame
    void Update()
    {
		if (vrInputModule.enabled)
		{
			eventSystemObject.SetActive(false);
			eventSystemObject.SetActive(true);

			standardInputModuleCopy.enabled = true;
			vrInputModule.enabled = false;

			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(joinMenuTransform.Find(rooms[0].name).Find("Button1").gameObject);
		}

		if (!searchText.Equals(search.text))
		{
			Search();
		}
	}

	void Search()
	{
		searchText = search.text;

		List<Room> searchRooms = new List<Room>();

		foreach (Room room in allRooms)
		{
			if (room.name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
			{
				searchRooms.Add(room);
			}
		}

		rooms = searchRooms.GetRange(0, Math.Min(5, searchRooms.Count));
		SetupRooms();
		UpdateTransitions();
	}

	void UpdateTransitions()
	{
		for (int i = 0; i < rooms.Count; i++)
		{
			Debug.Log(joinMenuTransform.Find(rooms[i].name).gameObject.name);
			Navigation nav = joinMenuTransform.Find(rooms[i].name).GetChild(1).gameObject.GetComponent<Button>().navigation;
			if (i == 0)
			{
				nav.selectOnLeft = search;
			}
			else
			{
				nav.selectOnLeft = joinMenuTransform.Find(rooms[i - 1].name).GetChild(1).gameObject.GetComponent<Button>();
			}
			if (i < rooms.Count - 1)
			{
				nav.selectOnRight = joinMenuTransform.Find(rooms[i + 1].name).GetChild(1).gameObject.GetComponent<Button>();
			}
			else
			{
				nav.selectOnRight = joinMenuTransform.Find("BackButton").gameObject.GetComponent<Button>();
			}
			joinMenuTransform.Find(rooms[i].name).GetChild(1).gameObject.GetComponent<Button>().navigation = nav;
		}

		Navigation nav1 = search.navigation;
		Navigation nav2 = joinMenuTransform.Find("BackButton").gameObject.GetComponent<Button>().navigation;
		if (rooms.Count > 0)
		{
			nav1.selectOnRight = joinMenuTransform.Find(rooms[0].name).GetChild(1).gameObject.GetComponent<Button>();

			nav2.selectOnLeft = joinMenuTransform.Find(rooms[rooms.Count - 1].name).GetChild(1).gameObject.GetComponent<Button>();
		}
		else
		{
			nav1.selectOnRight = joinMenuTransform.Find("BackButton").gameObject.GetComponent<Button>();

			nav2.selectOnLeft = search;
		}
		search.navigation = nav1;
		joinMenuTransform.Find("BackButton").gameObject.GetComponent<Button>().navigation = nav2;
	}

	void SetupRooms()
	{
		for (int i = 1; i < 6; i++)
		{
			if (i - 1 < rooms.Count)
			{
				if (!joinMenuTransform.GetChild(i).gameObject.activeSelf)
				{
					joinMenuTransform.GetChild(i).gameObject.SetActive(true);
				}
				joinMenuTransform.GetChild(i).gameObject.name = rooms[i - 1].name;

				string roomName = rooms[i - 1].name.Substring(0, Math.Min(rooms[i - 1].name.Length, 11));
				if (rooms[i - 1].name.Length > 11)
				{
					roomName += "...";
				}
				else
				{
					roomName += "   ";
				}

				string roomMovie = rooms[i - 1].movie.Substring(0, Math.Min(rooms[i - 1].movie.Length, 24));
				if (rooms[i - 1].movie.Length > 24)
				{
					roomMovie += "...";
				}

				string roomStr;
				if (rooms[i - 1].isPublic)
				{
					roomStr = string.Format("{0}<>   Public   <>   {1}", roomName, roomMovie);
				}
				else
				{
					roomStr = string.Format("{0}<>   Private  <>   {1}", roomName, roomMovie);
				}
				joinMenuTransform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = roomStr;
			}
			else
			{
				joinMenuTransform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}

}
