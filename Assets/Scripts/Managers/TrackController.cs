using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class TrackController : MonoBehaviour
{
    
    TextMeshProUGUI guesses;
    TextMeshProUGUI guessResult;
    TextMeshProUGUI hpText;
    TextMeshProUGUI[] textObjects;
    TMP_InputField userGuess;

    public TMP_InputField UserGuess{
        get{ return userGuess; }
    }
    AudioController audios;
    GameManager gm;
    int hp=0;
    public int HP{
        get{ return hp; }
        set{ hp = value; }
    }
    int range=0;
    [SerializeField]
    int numToGuess;

    [SerializeField]
    int id;

    void OnEnable(){
        gm = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<GameManager>();
        textObjects = gameObject.transform.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI textObject in textObjects)
        {
            guesses = textObject.name == "Tries" ? textObject : guesses;
            guessResult = textObject.name == "CorrectOrNot" ? textObject : guessResult;
            hpText = textObject.name == "Hp" ? textObject : hpText;
        }
        userGuess = gameObject.transform.GetComponentInChildren<TMP_InputField>();
        InitializeValues();
    }

    public void InitializeValues(){
        hp = gm.HP;
        range = gm.CurrentNumberLimit;
        numToGuess = NumGenerator.GenerateNumber(range);
        if (range != 10 && range != 100 && range != 1000)
        {
            hp = gm.SetHPS(id);
        }
        hpText.text = hp.ToString();
    }

    public void LevelUp(){
        hp += gm.HP;
        range += gm.NumberLimit;
        gm.CurrentNumberLimit = range;
        numToGuess = NumGenerator.GenerateNumber(range);
        hpText.text = hp.ToString();
    }

    public void HandleInput()
    {
        int userInputNumber;
        if (Int32.TryParse(userGuess.text, out userInputNumber))
        {
            HandleText(userInputNumber);
        }
    }

    private void HandleText(int userInputNumber){
        userGuess.text = "";

        if (numToGuess > userInputNumber)
        {
            guesses.text += $"<color=red>{userInputNumber}\n";
            guessResult.text = "<color=red>X ↑";
            HandleHP();
            gm.PlayerGotItWrong();
        }
        else if (numToGuess < userInputNumber)
        {
            guesses.text += $"<color=red>{userInputNumber}\n";
            guessResult.text = "<color=red>X ↓";
            HandleHP();
            gm.PlayerGotItWrong();
        }
        else
        {
            guesses.text +=$"<color=green>{userInputNumber}\n";
            guessResult.text = "<color=green>O";
            DeactivateTrack();
            gm.PlayerGotItRight();
            gm.CheckForWin();
        }
    }

    private void HandleHP(){
        hp--;
        hpText.text = hp.ToString();
        if (hp==0){
            DeactivateTrack();
            gm.CheckForDeath();
        }
    }

    private void DeactivateTrack(){
        userGuess.interactable = false;
    }

    public void CleanTrack(){
        guesses.text = "";
        userGuess.interactable = true;
        guessResult.text = "";
    }

    
    

}
