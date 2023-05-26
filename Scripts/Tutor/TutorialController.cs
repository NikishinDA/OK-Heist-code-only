using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private string tutorName;
    private void Awake()
    {
        if (string.IsNullOrEmpty(tutorName))
        {
            throw new ArgumentNullException();
        }
        if (PlayerPrefs.GetInt("Tutorial" + tutorName, 0) == 1)
            gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            PlayerPrefs.SetInt("Tutorial" + tutorName, 1);
        }
    }
}
