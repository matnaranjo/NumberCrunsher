using TMPro;
using UnityEngine;

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
}
