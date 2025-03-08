using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int hp;  // Variable to store player health points
    public int HP{
        get { return hp; }
        set { hp = value; }
    }

    int numberLimit;  // Variable to store the limit for numbers
    public int NumberLimit{
        get { return numberLimit; }
        set { numberLimit = value; }
    }

    int highScore;  // Variable to store the high score
    public int HighScore{
        get { return highScore; }
        set { highScore = value; }
    }

    int score = 0;  // Variable to store the current score
    public int Score{
        get { return score; }
        set { score = value; }
    }

    int currentNumberLimit;  // Variable to store the current number limit
    public int CurrentNumberLimit{
        get { return currentNumberLimit; }
        set { currentNumberLimit = value; }
    }

    List<int> hpsList = new List<int>();  // List to store the health points for each track
    
    [SerializeField]
    UIGame uiController;  // Reference to the UI controller
    [SerializeField]
    AudioController audioController;  // Reference to the audio controller
    GameObject[] trackList;  // Array to store all the track game objects
    Button submitButton;  // Reference to the submit button

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("maxscore", 0);  // Get the stored high score
        uiController.SetMaxScoreText(highScore);  // Set the max score text in the UI
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInput();  // Check for input to enable/disable the submit button
    }

    // Check if all input fields are filled and enable/disable the submit button accordingly
    private void CheckForInput(){
        if (submitButton != null && submitButton.gameObject.activeSelf == true){
            trackList = GameObject.FindGameObjectsWithTag("track");  // Get all track objects in the scene
            foreach (GameObject track in trackList){
                TMP_InputField input = track.GetComponent<TrackController>().UserGuess;  // Get the user input field
                if (input.text == "" && input.interactable != false){
                    submitButton.interactable = false;  // Disable the submit button if any input field is empty
                    return;
                }
            }
            submitButton.interactable = true;  // Enable the submit button if all input fields are filled
        }
    }

    // Get the reference to the submit button and disable it initially
    public void GetButton(Button button){
        submitButton = GameObject.FindGameObjectWithTag("submit").GetComponent<Button>();
        submitButton.interactable = false;  // Initially disable the submit button
    }

    // Handle the death scenario (player lost)
    public void CheckForDeath(){
        SaveHighScore();  // Save the high score if needed
        uiController.PlayerLost();  // Trigger the UI for player lost
    }

    // Check if all tracks are completed (player won)
    public void CheckForWin(){
        foreach (GameObject track in trackList)
        {
            if (track.GetComponent<TrackController>().UserGuess.interactable == true){
                return;  // If any track's user input is still interactable, the player hasn't won
            }
        }
        SetScore();  // Set the final score after winning
        SaveHighScore();  // Save the high score if needed
        uiController.PlayerWon();  // Trigger the UI for player won
    }

    // Handle the continuation of the player (level up)
    public void PlayerContinued(){
        EnableTrack();  // Enable the tracks for the next level
        LevelUP();  // Increase the level of all tracks
        SaveInfo();  // Save the current game info
    }

    // Level up all tracks
    public void LevelUP(){
        foreach (GameObject track in trackList)
        {
            track.GetComponent<TrackController>().LevelUp();  // Level up each track
        }
    }

    // Reset the game (clear tracks, reset score)
    public void Reset(){
        foreach (GameObject track in trackList)
        {
            track.GetComponent<TrackController>().InitializeValues();  // Initialize the track values
        }
        score = 0;  // Reset the score
        uiController.SetScoreText(score);  // Update the score UI
    }

    // Set the score based on the remaining HP of the tracks
    private void SetScore(){
        int remainingHp = 0;

        foreach (GameObject track in trackList)
        {
            remainingHp += track.GetComponent<TrackController>().HP;  // Sum up the remaining HP of all tracks
        }

        score += (remainingHp * 10) + ((remainingHp / 3) * 50);  // Calculate the score
        uiController.SetScoreText(score);  // Update the score UI
    }

    // Save the high score if the current score is higher
    private void SaveHighScore(){
        if (score > highScore){
            PlayerPrefs.SetInt("maxscore", score);  // Save the new high score
            uiController.SetMaxScoreText(score);  // Update the high score UI
        }
    }

    // Save the player's game data (HP, score, etc.)
    private void SaveInfo(){
        switch (hp){
            case 5:
                PlayerPrefs.SetInt("Dif", 1);  // Save difficulty level based on HP
                break;
            case 7:
                PlayerPrefs.SetInt("Dif", 2);
                break;
            case 11:
                PlayerPrefs.SetInt("Dif", 3);
                break;
        }

        string hps = "";
        foreach (GameObject track in trackList)
        {
            hps += track.GetComponent<TrackController>().HP.ToString() + " ";  // Save the HP of each track
        }
        PlayerPrefs.SetInt("hp", hp);  // Save player HP
        PlayerPrefs.SetString("hps", hps);  // Save all track HP values
        PlayerPrefs.SetInt("limitIncrease", numberLimit);  // Save number limit
        PlayerPrefs.SetInt("limit", currentNumberLimit);  // Save current number limit
        PlayerPrefs.SetInt("score", score);  // Save current score
    }

    // Load the saved level data 
    public void LoadLevel(){
        hp = PlayerPrefs.GetInt("hp");  // Load player HP
        score = PlayerPrefs.GetInt("score");  // Load player score
        numberLimit = PlayerPrefs.GetInt("limitIncrease");  // Load number limit
        currentNumberLimit = PlayerPrefs.GetInt("limit");  // Load current number limit

        uiController.LevelSelected(PlayerPrefs.GetInt("Dif"));  // Set the level based on saved difficulty
        uiController.SetScoreText(score);  // Update the score UI
    }

    // Set the HP for a specific track based on saved data
    public int SetHPS(int id){
        string hps = PlayerPrefs.GetString("hps");  // Get the saved HP string
        string trackHP = "";
        foreach (char letter in hps)
        {
            if (letter != ' '){
                trackHP += letter;  // Build the track HP value string
            }
            else{
                hpsList.Add(Int32.Parse(trackHP));  // Add the HP value to the list
                Debug.Log($"{hpsList.Count}");  // Debug the list count
                trackHP = "";
            }
        }
        return hpsList[id];  // Return the HP value for the specified track
    }

    // Enable the tracks for the next level
    private void EnableTrack(){
        foreach(GameObject track in trackList){
            track.GetComponent<TrackController>().CleanTrack();  // Clean up the track before enabling it for the next level
        }
    }
}
