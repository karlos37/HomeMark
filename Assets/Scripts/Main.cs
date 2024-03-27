using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Main : MonoBehaviour
{
    [SerializeField] private Canvas mainMenu;
	[SerializeField] private Canvas joinMenu;

    [SerializeField] private GameObject theater;
	[SerializeField] private GameObject reticle;

	[SerializeField] private XRCardboardInputModule vrInputModule;
	[SerializeField] private StandaloneInputModuleCopy standardInputModuleCopy;

	private string[] mainMenuNames;
	private string[] joinMenuNames;

	// Start is called before the first frame update
	void Start()
    {
		mainMenu.enabled = true;
		joinMenu.enabled = false;
		theater.SetActive(false);

		mainMenuNames = new string[2] { "Create Room Button", "Join Room Button" };
		joinMenuNames = new string[2] { "Button", "Button (1)" };
	}

    // Update is called once per frame
    void Update()
    {
		if (mainMenu.enabled)
		{
			if (vrInputModule.enabled)
			{
				standardInputModuleCopy.enabled = true;
				vrInputModule.enabled = false;

				EventSystem.current.SetSelectedGameObject(null);
				EventSystem.current.SetSelectedGameObject(mainMenu.transform.Find("IntroPanel").Find("Create Room Button").gameObject);
			}

			navigateMainMenu();
		}
		else if (joinMenu.enabled)
		{
			navigateJoinMenu();
		}
		

	}

    void navigateMainMenu()
    {
		if (Input.GetKeyDown("m"))
		{
			if (EventSystem.current.currentSelectedGameObject == mainMenu.transform.Find("IntroPanel").Find("Join Room Button").gameObject)
			{
				mainMenu.enabled = false;
				joinMenu.enabled = true;

				EventSystem.current.SetSelectedGameObject(null);
				EventSystem.current.SetSelectedGameObject(joinMenu.transform.Find("JoinPanel").Find("Room1").Find("Button").gameObject);
			}
		}
	}

	void navigateJoinMenu()
	{
		if (Input.GetKeyDown("m"))
		{
			if (EventSystem.current.currentSelectedGameObject == joinMenu.transform.Find("JoinPanel").Find("Room1").Find("Button").gameObject)
			{
				mainMenu.enabled = false;
				joinMenu.enabled = false;
				theater.SetActive(true);

				standardInputModuleCopy.enabled = false;
				vrInputModule.enabled = true;
				reticle.SetActive(true);
			}
		}
	}
}
