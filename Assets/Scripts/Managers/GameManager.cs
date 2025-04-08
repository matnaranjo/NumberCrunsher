using System;
using System.Collections.Generic;
using System.Linq;
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

    private int activeTracksCount = 0;
    private static int misses = 0;

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

    public void CheckForWin()   {
        foreach (GameObject track in trackList)
        {
            if (track.GetComponent<TrackController>().UserGuess.interactable == true)
            {
                return;
            }
        }
        SetScore();
        SaveHighScore();
        uiController.PlayerWon();
    }

    public void PlayerContinued()
    {
        uiController.PlayerContinued();
        LevelUP();
        SaveInfo();
        EnableTrack();
    }
    public void LevelUP(){
        foreach (GameObject track in trackList)
        {
            track.GetComponent<TrackController>().LevelUp();
        }

        uiController.SetRangeText(currentNumberLimit);
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

    private void SaveInfo()
    {
        switch (hp)
        {
            case 20:
                PlayerPrefs.SetInt("Dif", 1);
                break;
            case 7:
                PlayerPrefs.SetInt("Dif", 2);
                break;
            case 11:
                PlayerPrefs.SetInt("Dif", 3);
                break;
        }

        PlayerPrefs.SetInt("hp", hp);  // Make sure this is being saved correctly
        PlayerPrefs.SetInt("limitIncrease", numberLimit);
        PlayerPrefs.SetInt("limit", currentNumberLimit);
        PlayerPrefs.SetInt("score", score);

        string hps = "";
        foreach (GameObject track in trackList)
        {
            hps += track.GetComponent<TrackController>().HP.ToString() + " ";
        }
        PlayerPrefs.SetString("hps", hps);
    }

    public void LoadLevel()
    {
        // Load from PlayerPrefs first
        hp = PlayerPrefs.GetInt("hp", 5);  // Default to 5 if not set
        score = PlayerPrefs.GetInt("score");
        numberLimit = PlayerPrefs.GetInt("limitIncrease");
        currentNumberLimit = PlayerPrefs.GetInt("limit");


        // If PlayerPrefs doesn't have the correct value, set it based on difficulty
        int difficulty = PlayerPrefs.GetInt("Dif");
        switch (difficulty)
        {
            case 1:
                hp = 5;
                break;
            case 2:
                hp = 7;
                break;
            case 3:
                hp = 11;
                break;
        }

        // Set the level UI and score
        Debug.Log("A");
        uiController.LevelSelected(difficulty);
        Debug.Log("B");
        uiController.SetScoreText(score);
        uiController.SetRangeText(currentNumberLimit);

        // Pass the HP value to the tracks
        foreach (GameObject track in trackList)
        {
            track.GetComponent<TrackController>().InitializeValues();  // This will now use the updated `gm.HP` value
        }
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

    public void PlayerGotItRight()
    {
        activeTracksCount--;
    }

    public void PlayerGotItWrong()
    {
        misses++;
    }


    public void StartTurn()
    {
        misses = 0;
        activeTracksCount = 0;        
    }

    public void EndTurn()
    {
        
        foreach (GameObject track in trackList)
        {
            TrackController trackController = track.GetComponent<TrackController>();
            if (trackController.UserGuess.interactable) // If the track is still active
            {
                activeTracksCount++;
            }
        }

        if (activeTracksCount == misses)
        {
            uiController.PlayerFailedTurn();
        }
        else
        {
            
            uiController.PlayerGotAtLeastOneRight();
        }
    }
}
