using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int hp;
    public int HP{
        get { return hp; }
        set { hp = value; }
    }
    int numberLimit;
    public int NumberLimit{
        get { return numberLimit; }
        set { numberLimit = value; }
    }

    int highScore;
    public int HighScore{
        get { return highScore; }
        set { highScore = value; }
    }
    int score=0;
    public int Score{
        get { return score; }
        set { score = value; }
    }

    int currentNumberLimit;
    public int CurrentNumberLimit{
        get { return currentNumberLimit; }
        set {currentNumberLimit = value;}
    }

    List<int> hpsList = new List<int>();
    
    [SerializeField]
    UIGame uiController;
    [SerializeField]
    AudioController audioController;
    GameObject[] trackList;
    Button submitButton;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("maxscore", 0);
        uiController.SetMaxScoreText(highScore);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        CheckForInput();
    }

    private void CheckForInput(){
        if (submitButton!=null && submitButton.gameObject.activeSelf == true){
            trackList = GameObject.FindGameObjectsWithTag("track");
            foreach (GameObject track in trackList){
                TMP_InputField input = track.GetComponent<TrackController>().UserGuess;
                if (input.text == "" && input.interactable!=false){
                    submitButton.interactable = false;
                    return;
                }
            }
            submitButton.interactable = true;
        }
    }
    public void GetButton(Button button){
        submitButton = GameObject.FindGameObjectWithTag("submit").GetComponent<Button>();
        submitButton.interactable = false;
    }

    public void CheckForDeath(){
        SaveHighScore();
        uiController.PlayerLost();
    }

    public void CheckForWin(){
        foreach (GameObject track in trackList)
        {
            if (track.GetComponent<TrackController>().UserGuess.interactable ==true){
                return;
            }
        }
        SetScore();
        SaveHighScore();
        uiController.PlayerWon();
    }

    public void PlayerContinued(){
        LevelUP();
        SaveInfo();
        EnableTrack();
    }
    public void LevelUP(){
        foreach (GameObject track in trackList)
        {
            track.GetComponent<TrackController>().LevelUp();
        }
    }

    public void Reset(){
        foreach (GameObject track in trackList)
        {
            track.GetComponent<TrackController>().InitializeValues();
        }
        score =0;
        uiController.SetScoreText(score);
    }

    private void SetScore(){
        int remainingHp = 0;

        foreach (GameObject track in trackList)
        {
            remainingHp += track.GetComponent<TrackController>().HP;
        }

        score += (remainingHp*10) + ((remainingHp/3)*50);
        uiController.SetScoreText(score);
    }

    private void SaveHighScore(){
        if (score > highScore){
            PlayerPrefs.SetInt("maxscore", score);
            uiController.SetMaxScoreText(score);
        }
    }

    private void SaveInfo(){
        switch (hp){
            case 5:
            PlayerPrefs.SetInt("Dif",1);
            break;
            case 7:
            PlayerPrefs.SetInt("Dif",2);
            break;
            case 11:
            PlayerPrefs.SetInt("Dif",3);
            break;
        }

        string hps = "";
        foreach (GameObject track in trackList)
        {
            hps += track.GetComponent<TrackController>().HP.ToString()+" ";
        }
        PlayerPrefs.SetInt("hp", hp);
        PlayerPrefs.SetString("hps", hps);
        PlayerPrefs.SetInt("limitIncrease", numberLimit);
        PlayerPrefs.SetInt("limit", currentNumberLimit);
        PlayerPrefs.SetInt("score", score);
    }

    public void LoadLevel(){
        hp = PlayerPrefs.GetInt("hp");
        score =PlayerPrefs.GetInt("score");
        numberLimit = PlayerPrefs.GetInt("limitIncrease");
        currentNumberLimit = PlayerPrefs.GetInt("limit");

        uiController.LevelSelected(PlayerPrefs.GetInt("Dif"));
        uiController.SetScoreText(score);
    }
    public int SetHPS(int id){
        string hps = PlayerPrefs.GetString("hps");
        string trackHP="";
        foreach (char letter in hps)
        {
            if (letter!=' '){
                trackHP += letter;
            }
            else{
                hpsList.Add(Int32.Parse(trackHP));
                Debug.Log($"{hpsList.Count}");
                trackHP = "";
            }
        }
        return hpsList[id];
    }

    private void EnableTrack(){
        foreach(GameObject track in trackList){
            track.GetComponent<TrackController>().CleanTrack();
        }
    }

}
