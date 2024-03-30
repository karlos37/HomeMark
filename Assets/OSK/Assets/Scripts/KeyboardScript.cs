using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class KeyboardScript : MonoBehaviour
{

    public TMP_InputField TextField;
    public GameObject EngLayoutSml, SymbLayout;

	[SerializeField] private Transform joinMenuTransform;

	public void alphabetFunction(string alphabet)
    {


        TextField.text=TextField.text + alphabet;

    }

    public void BackSpace()
    {

        if(TextField.text.Length>0) TextField.text= TextField.text.Remove(TextField.text.Length-1);

    }

    public void CloseAllLayouts()
    {

		EngLayoutSml.SetActive(false);
        SymbLayout.SetActive(false);

		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(joinMenuTransform.GetChild(1).Find("Button1").gameObject);
	}

    public void ShowLayout(GameObject SetLayout)
    {

        CloseAllLayouts();
        SetLayout.SetActive(true);

    }

}
