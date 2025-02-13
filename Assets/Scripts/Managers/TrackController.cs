using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class TrackController : MonoBehaviour
{
    
    TextMeshProUGUI guesses;
    TextMeshProUGUI guessResult;
    TextMeshProUGUI hpText;
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
            guessResult = textObject.name == "CorrectOrNot" ? textObject : guessResult;
            hpText = textObject.name == "Hp" ? textObject : hpText;
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
        hpText.text = hp.ToString();
    }

    private void HandleInput(){
        int userInputNumber;
        if (Int32.TryParse(userGuess.text, out userInputNumber)){
            HandleText(userInputNumber);
        }
    }

    private void HandleText(int userInputNumber){
        userGuess.text = "";
        //Equal
        if (numToGuess == userInputNumber){
            guesses.text +=$"<color=green>{userInputNumber}\n";
            guessResult.text = "<color=green>O";
            DeactivateTrack();
        }
        //bigger
        else if(numToGuess>userInputNumber){
            guesses.text +=$"<color=red>{userInputNumber}\n";
            guessResult.text = "<color=red>X ↑";
            HandleHP();
        }
        //Smaller
        else{
            guesses.text +=$"<color=red>{userInputNumber}\n";
            guessResult.text = "<color=red>X ↓";
            HandleHP();
        }
    }

    private void HandleHP(){
        hp--;
        hpText.text = hp.ToString();
        if (hp==0){
            DeactivateTrack();
        }
    }

    private void DeactivateTrack(){
        submitButton.interactable = false;
        userGuess.interactable = false;
    }

    
}
