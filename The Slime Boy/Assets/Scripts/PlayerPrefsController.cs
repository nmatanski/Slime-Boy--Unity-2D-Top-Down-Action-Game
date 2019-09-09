using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerPrefsController : MonoBehaviour
{
    public string GetName()
    {
        var player = PlayerPrefs.GetString("PlayerName");
        if (!PlayerPrefs.HasKey("PlayerName") || string.IsNullOrWhiteSpace(player))
        {
            return "";
        }
        return player;
    }

    public void SetName()
    {
        Debug.Log(PlayerPrefs.GetString("PlayerName"));

        if (PlayerPrefs.HasKey("PlayerName") && !string.IsNullOrWhiteSpace(PlayerPrefs.GetString("PlayerName")))
        {
            return;
        }

        var field = GetComponent<TMP_InputField>();

        PlayerPrefs.SetString("PlayerName", field.text);

        Debug.Log(PlayerPrefs.GetString("PlayerName"));
    }

    public int GetScore()
    {
        if (!PlayerPrefs.HasKey("PlayerScore"))
        {
            return 0;
        }

        return PlayerPrefs.GetInt("PlayerScore");
    }

    public void SetScore(int score)
    {
        PlayerPrefs.SetInt("PlayerScore", score);
    }
}
