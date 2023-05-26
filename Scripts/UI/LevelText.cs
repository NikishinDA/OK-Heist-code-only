using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    [SerializeField] private Text levelText;

    private void Start()
    {
        levelText.text =
            "LEVEL " + (PlayerPrefs.GetInt(PlayerPrefsStrings.Level.Name, PlayerPrefsStrings.Level.DefaultValue) -
                        2 * PlayerPrefs.GetInt(PlayerPrefsStrings.TimesRotated.Name,
                            PlayerPrefsStrings.TimesRotated.DefaultValue));
    }
}