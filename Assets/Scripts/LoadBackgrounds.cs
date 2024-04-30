using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadBackgrounds : MonoBehaviour
{
    public GameObject backgroundListItemPrefab;
    public GameObject backgroundListParent;
    public CreateMenu createMenu;
    public Button backButton;
    
    private string[] _background;
    private Button[] _buttons;
    private int _selectedButtonIndex = -1;

    private Array _bgs;
    // Start is called before the first frame update
    void Start()
    {
        _bgs = Enum.GetValues(typeof(Room.Background));
        _buttons = new Button[_bgs.Length + 1];
        for (int i = 0; i < _bgs.Length; i++)
        {
            Room.Background bg = (Room.Background) _bgs.GetValue(i);
            GameObject backgroundItem = Instantiate(backgroundListItemPrefab, backgroundListParent.transform);
            TextMeshProUGUI nameText = backgroundItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            nameText.text = "  " + bg;
            Button button = backgroundItem.GetComponent<Button>();
            _buttons[i] = button;
            int index = i;
            button.onClick.AddListener(() => OnBackgroundSelected(index));
        }
        _buttons[^1] = backButton;
        _selectedButtonIndex = 0;

		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(_buttons[_selectedButtonIndex].gameObject);
	}

    // Update is called once per frame
    void Update()
    {
        // Return buttons does not exist
        if (_buttons == null || _buttons.Length == 0)
        {
            return;
        }
        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)
        {
            _selectedButtonIndex = (_selectedButtonIndex - 1 + _buttons.Length) % _buttons.Length;
        }

        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0)
        {
            _selectedButtonIndex = (_selectedButtonIndex + 1 + _buttons.Length) % _buttons.Length;
        }
        
        EventSystem.current.SetSelectedGameObject(null);
        if (_selectedButtonIndex == -1)
        {
            return;
        }
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_buttons[_selectedButtonIndex].gameObject);
    }

    void OnBackgroundSelected(int i)
    {
        print("Selected background " + (Room.Background)_bgs.GetValue(i));
        createMenu.OnBackgroundSelected((Room.Background)_bgs.GetValue(i));
    }
    
}
