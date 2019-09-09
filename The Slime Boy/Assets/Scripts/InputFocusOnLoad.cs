using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputFocusOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var field = GetComponent<TMP_InputField>();

        if (PlayerPrefs.HasKey("PlayerName") && PlayerPrefs.GetString("PlayerName").Length > 0)
        {
            field.placeholder.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("PlayerName");
            GameObject.FindGameObjectWithTag("NameInputTextObject").SetActive(false);
        }
        else
        {
            field.Select();
            field.ActivateInputField();
        }
    }
}
