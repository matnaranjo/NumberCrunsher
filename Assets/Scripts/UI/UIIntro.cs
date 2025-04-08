using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIIntro : MonoBehaviour
{
    [SerializeField]
    GameObject nameInput;  // GameObject for the name input screen
    [SerializeField]
    GameObject welcome;  // GameObject for the welcome message screen
    [SerializeField]
    TextMeshProUGUI nameDisplay;  // TextMeshProUGUI component to display the user's name
    [SerializeField]
    GameObject clickToContinue;  // GameObject for the "click to continue" button
    [SerializeField]
    GameObject HowTo;  // GameObject for the HowTo panel that displays the instructions

private void Start()
    {
        // Ensure the HowTo panel is hidden at the start
        if (HowTo != null)
        {
            HowTo.SetActive(false);
        }
    }
    // Called when the user has already entered a name
    public void UserHasName(){
        nameInput.SetActive(false);  // Hide the name input screen
        welcome.SetActive(true);  // Show the welcome message
        clickToContinue.SetActive(true);  // Show the "click to continue" button
    }

    // Called when the user has not entered a name yet
    public void UserHasNoName(){
        nameInput.SetActive(true);  // Show the name input screen
        welcome.SetActive(false);  // Hide the welcome message
    }

    // Updates the name display with the given name
    public void GetNameAndDisplay(string name){
        nameDisplay.text = nameDisplay.text + name;  // Display the user's name on the UI
    }

    // Called when the HowTo button is clicked
    public void OnButtonHowToClick()
    {
        welcome.SetActive(false);  // Hide the welcome screen
        HowTo.SetActive(true);  // Show the HowTo panel with instructions
        clickToContinue.SetActive(false);  // Hide the "click to continue" button while in HowTo panel
    }

    // Called when the Back button on the HowTo panel is clicked
    public void OnBackClick()
    {
        // Load the Intro scene
        SceneManager.LoadScene("Intro");
    }
}
