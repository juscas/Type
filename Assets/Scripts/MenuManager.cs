﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Text typedWord;
    public InputField inputField;
    private string typedText;
    private RectTransform rect;
    
    private Vector3 rectPos;

    private const string PLAY = "play", OPTIONS = "options", LEADERBOARDS = "leaderboards";
    void Start()
    {
		typedWord = GameObject.FindGameObjectWithTag("Input").GetComponent<Text> ();
        GameObject iField = GameObject.Find("InputField");
        inputField = iField.GetComponent<InputField>();
        rect = iField.GetComponent<RectTransform>();
        inputField.Select();
    }

    // Update is called once per frame
    void Update()
    {
        typedText = inputField.text;
        //Debug.Log(typedText);
        GameObject iField = GameObject.Find("InputField");
        Debug.Log(rect.localPosition.y);

        GameObject enter = GameObject.Find("Enter");
        Text enterText = enter.GetComponent<Text>();

        if (typedText == PLAY){
            rectPos = new Vector3(-203,108,0);
            rect.localPosition = rectPos;
            enterText.text = "Press enter";
            if (Input.GetButtonDown("Submit")){
                Debug.Log("Pressed Enter");
                SceneManager.LoadScene("PlayMenu",LoadSceneMode.Single);
            }
            return;
        }
        if (typedText == OPTIONS){
            rectPos = new Vector3(-203,48,0);
            rect.localPosition = rectPos;
            enterText.text = "Press enter";
            return;
        }
        if (typedText == LEADERBOARDS){
            rectPos = new Vector3(-203,-12,0);
            rect.localPosition = rectPos;
            enterText.text = "Press enter";
            return;
        }
        enterText.text = "";
    }
}
