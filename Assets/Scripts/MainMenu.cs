using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	[SerializeField] private XRCardboardInputModule vrInputModule;
	[SerializeField] private StandaloneInputModuleCopy standardInputModuleCopy;
	[SerializeField] private GameObject eventSystemObject;

	// Start is called before the first frame update
	void Start()
    {
		eventSystemObject.SetActive(false);
		eventSystemObject.SetActive(true);

		standardInputModuleCopy.enabled = true;
		vrInputModule.enabled = false;

		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(transform.Find("IntroPanel").Find("Create Room Button").gameObject);
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
			EventSystem.current.SetSelectedGameObject(transform.Find("IntroPanel").Find("Create Room Button").gameObject);
		}
	}

    public void gotoJoinMenu()
    {
		SceneManager.LoadScene("JoinMenu");
	}

	public void gotoCreateMenu()
	{
		SceneManager.LoadScene("CreateMenu");
	}
}
