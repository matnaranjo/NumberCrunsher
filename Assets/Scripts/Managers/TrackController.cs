using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class TrackController : MonoBehaviour
{
    TextMeshProUGUI guesses;  // Text component to show the guesses
    TextMeshProUGUI guessResult;  // Text component to show the result of the guess (correct or not)
    TextMeshProUGUI hpText;  // Text component to show the track's health points (HP)
    TextMeshProUGUI[] textObjects;  // Array of all TextMeshProUGUI objects in the track
    TMP_InputField userGuess;  // Input field for the player's guess
    public TMP_InputField UserGuess{
        get{ return userGuess; }
    }
    AudioController audios;  // Reference to the audio controller (if needed)
    GameManager gm;  // Reference to the GameManager to get game state and data
    int hp = 0;  // Track's health points
    public int HP{
        get{ return hp; }
        set{ hp = value; }
    }
    int range = 0;  // Range for the number the player has to guess
    [SerializeField]
    int numToGuess;  // The number the player has to guess
    [SerializeField]
    int id;  // Unique ID for the track (used for saving and loading HP)

    // Called when the object is enabled
    void OnEnable(){
        gm = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<GameManager>();  // Get the GameManager
        textObjects = gameObject.transform.GetComponentsInChildren<TextMeshProUGUI>();  // Get all TextMeshProUGUI components in the track
        foreach (TextMeshProUGUI textObject in textObjects)
        {
            // Assign each text component to the corresponding variable based on the name of the text object
            guesses = textObject.name == "Tries" ? textObject : guesses;
            guessResult = textObject.name == "CorrectOrNot" ? textObject : guessResult;
            hpText = textObject.name == "Hp" ? textObject : hpText;
        }
        userGuess = gameObject.transform.GetComponentInChildren<TMP_InputField>();  // Get the input field for the user's guess
        InitializeValues();  // Initialize the track's values (HP, range, etc.)
    }

    // Initialize the track's values
    public void InitializeValues(){
        hp = gm.HP;  // Set the track's HP from the GameManager
        range = gm.CurrentNumberLimit;  // Set the range from the GameManager
        Debug.Log(range);  // Log the range for debugging
        numToGuess = NumGenerator.GenerateNumber(range);  // Generate a number to guess based on the range
        guesses.text =$"<color=yellow>{numToGuess}\n";  // Shows number to guess
        if (range != 10 && range != 100 && range != 1000){
            hp = gm.SetHPS(id);  // Set the HP from the saved data if the range is not a standard range
        }
        hpText.text = hp.ToString();  // Update the HP text on the UI
    }

    // Level up the track (increase HP and range)
    public void LevelUp(){
        hp += gm.HP;  // Increase the HP
        range += gm.NumberLimit;  // Increase the range
        gm.CurrentNumberLimit = range;  // Update the range in the GameManager
        numToGuess = NumGenerator.GenerateNumber(range);  // Generate a new number to guess
        guesses.text =$"<color=yellow>{numToGuess}\n";  // Shows number to guess
        hpText.text = hp.ToString();  // Update the HP text on the UI
    }

    // Handle the user's input (guess)
    public void HandleInput(){
        int userInputNumber;
        if (Int32.TryParse(userGuess.text, out userInputNumber)){
            HandleText(userInputNumber);  // Handle the guess if it's a valid number
        }
    }

    // Handle the result of the user's guess
    private void HandleText(int userInputNumber){
        userGuess.text = "";  // Clear the input field

        // If the guess is correct
        if (numToGuess == userInputNumber){
            guesses.text +=$"<color=green>{userInputNumber}\n";  // Display the correct guess in green
            guessResult.text = "<color=green>O";  // Show the "O" for correct in green
            DeactivateTrack();  // Deactivate the track since the guess is correct
            gm.CheckForWin();  // Check if the player has won
        }
        // If the guess is too low
        else if (numToGuess > userInputNumber){
            guesses.text +=$"<color=red>{userInputNumber}\n";  // Display the incorrect guess in red
            guessResult.text = "<color=red>X ↑";  // Show the "X" for incorrect and indicate the guess was too low
            HandleHP();  // Decrease the track's HP
        }
        // If the guess is too high
        else{
            guesses.text +=$"<color=red>{userInputNumber}\n";  // Display the incorrect guess in red
            guessResult.text = "<color=red>X ↓";  // Show the "X" for incorrect and indicate the guess was too high
            HandleHP();  // Decrease the track's HP
        }
    }

    // Handle the track's HP decrease when the guess is incorrect
    private void HandleHP(){
        hp--;  // Decrease HP by 1
        hpText.text = hp.ToString();  // Update the HP text on the UI
        if (hp == 0){
            DeactivateTrack();  // Deactivate the track if HP reaches 0
            gm.CheckForDeath();  // Check if the player has lost
        }
    }

    // Deactivate the track (disable input and interactions)
    private void DeactivateTrack(){
        userGuess.interactable = false;  // Disable the input field
    }

    // Clean up the track (reset values for the next level or game)
    public void CleanTrack(){
        guesses.text = "";  // Clear the guesses text
        userGuess.interactable = true;  // Enable the input field
        guessResult.text = "";  // Clear the result text
    }
}
