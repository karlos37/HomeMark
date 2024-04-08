using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyboardOutline : MonoBehaviour
{
	[SerializeField] private Image outline;

	private GameObject highlightedButton;

	// Start is called before the first frame update
	void Start()
    {
		highlightedButton = null;
		outline.enabled = true;
	}

    // Update is called once per frame
    void Update()
    {
		if (EventSystem.current.currentSelectedGameObject.name == "Search" || EventSystem.current.currentSelectedGameObject.name == "RoomInput")
		{
			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(transform.GetChild(1).gameObject);
		}
		EnableButtonHighlight();
    }

	void EnableButtonHighlight()
	{
		if (EventSystem.current.currentSelectedGameObject != null && highlightedButton != EventSystem.current.currentSelectedGameObject)
		{
			if (EventSystem.current.currentSelectedGameObject.layer == 6)
			{
				outline.transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
				outline.transform.GetComponent<RectTransform>().sizeDelta = EventSystem.current.currentSelectedGameObject.transform.GetComponent<RectTransform>().sizeDelta;
				highlightedButton = EventSystem.current.currentSelectedGameObject;
			}

		}
	}
}
