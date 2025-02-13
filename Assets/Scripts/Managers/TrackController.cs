using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class TrackController : MonoBehaviour
{
    
    TextMeshProUGUI guesses;
    TextMeshProUGUI guessResult;
    TextMeshProUGUI[] textObjects;
    TMP_InputField userGuess;
    Button submitButton;
    AudioController audios;
    GameManager gm;
    int hp;
    int range;
    int numToGuess;



    void Start(){
        gm = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<GameManager>();
        textObjects = gameObject.transform.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI textObject in textObjects)
        {
            guesses = textObject.name == "Tries" ? textObject : guesses;
            guessResult = textObject.name == "CorrectOrNot" ? textObject : guesses;
        }
        userGuess = gameObject.transform.GetComponentInChildren<TMP_InputField>();
        submitButton = gameObject.transform.GetComponentInChildren<Button>();
        submitButton.onClick.AddListener(HandleInput);
        InitializeValues();
    }

    private void InitializeValues(){
        hp = gm.HP;
        range = gm.NumberLimit;
        numToGuess = NumGenerator.GenerateNumber(range);
    }

    private void HandleInput(){
        int userInputNumber;
        if (Int32.TryParse(userGuess.text, out userInputNumber)){
            HandleText(userInputNumber);
        }
    }

    private void HandleText(int userInputNumber){
        guesses.text +=$"{userInputNumber}\n";
        userGuess.text = "";
        //Equal
        if (numToGuess == userInputNumber){
            guessResult.text = "<color=green>O";
        }
        //bigger
        else if(numToGuess>userInputNumber){
            guessResult.text = "<color=red>X ↑";

        }
        //Smaller
        else{
            guessResult.text = "<color=red>X ↓";

        }
    }

    
}
