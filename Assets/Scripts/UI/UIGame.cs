using TMPro;
using UnityEngine;

public class UIGame : MonoBehaviour
{
    [SerializeField]
    GameObject selectLevel;  // GameObject for the level selection screen
    [SerializeField]
    GameObject easyScreen;  // GameObject for the easy difficulty screen
    [SerializeField]
    GameObject moderateScreen;  // GameObject for the moderate difficulty screen
    [SerializeField]
    GameObject DifficultScreen;  // GameObject for the difficult difficulty screen
    [SerializeField]
    GameObject defeat;  // GameObject for the defeat screen
    [SerializeField]
    GameObject pauseMenu;  // GameObject for the pause menu
    [SerializeField]
    GameObject continueNextLevel;  // GameObject for the continue next level button
    [SerializeField]
    GameObject uiGeneral;  // GameObject for the general UI during gameplay
    [SerializeField]
    TextMeshProUGUI score;  // TextMeshProUGUI component to display the current score
    [SerializeField]
    TextMeshProUGUI maxScore;  // TextMeshProUGUI component to display the max score

    // Called when a level is selected; shows the appropriate difficulty screen
    public void LevelSelected(int level){
        selectLevel.SetActive(false);  // Hide the level selection screen
        uiGeneral.SetActive(true);  // Show the general gameplay UI

        // Show the corresponding difficulty screen based on the selected level
        switch(level){
            case 1:
                easyScreen.SetActive(true);
                break;
            case 2:
                moderateScreen.SetActive(true);
                break;
            case 3:
                DifficultScreen.SetActive(true);
                break;
        }
    }

    // Called when the player wins the game
    public void PlayerWon(){
        continueNextLevel.SetActive(true);  // Show the "continue to next level" button
    }

    // Called when the player loses the game
    public void PlayerLost(){
        TurnOFOnGame();  // Turn off all game screens
        defeat.SetActive(true);  // Show the defeat screen
    }

    // Called when the player resets the game
    public void PlayerReset(){
        TurnOFOnGame();  // Turn off all game screens
        uiGeneral.SetActive(false);  // Hide the general gameplay UI
        pauseMenu.SetActive(false);  // Hide the pause menu
        selectLevel.SetActive(true);  // Show the level selection screen
    }

    // Called when the player pauses the game
    public void PlayerPaused(){
        pauseMenu.SetActive(true);  // Show the pause menu
    }

    // Called when the player unpauses the game
    public void PlayerUnpaused(){
        pauseMenu.SetActive(false);  // Hide the pause menu
    }

    // Called when the player continues to the next level
    public void PlayerContinued(){
        continueNextLevel.SetActive(false);  // Hide the "continue to next level" button
    }

    // Updates the score text on the UI
    public void SetScoreText(int score){
        this.score.text = $"Score: {score}";  // Set the current score text
    }

    // Updates the max score text on the UI
    public void SetMaxScoreText(int maxScore){
        this.maxScore.text = $"Max score: {maxScore}";  // Set the max score text
    }

    // Helper method to turn off all difficulty screens
    private void TurnOFOnGame(){
        bool onOff = false;
        easyScreen.SetActive(onOff);  // Turn off easy difficulty screen
        moderateScreen.SetActive(onOff);  // Turn off moderate difficulty screen
        DifficultScreen.SetActive(onOff);  // Turn off difficult difficulty screen
    }
    public void PlayerExit() {

          // Quit the game
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Stop play mode
        #else
        Application.Quit();  // Quit the game
        #endif
    }
}
