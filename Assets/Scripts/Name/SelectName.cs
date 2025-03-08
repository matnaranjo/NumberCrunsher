using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectName : MonoBehaviour
{
    [SerializeField]
    UIIntro uiController;  // Reference to the UI controller for handling UI updates
    [SerializeField]
    TMP_InputField txtName;  // Reference to the input field where the user enters their name
    private string userName;  // Variable to store the user's name

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        userName = PlayerPrefs.GetString("name", "");  // Retrieve the saved user name from PlayerPrefs
        if (userName == ""){
            uiController.UserHasNoName();  // If no name is saved, show the right UI
        }
        else{
            uiController.GetNameAndDisplay(userName);  // Display the saved name on the UI
            uiController.UserHasName();  // Inform the UI that the user has a name
        }
    }

    // Save the entered name to PlayerPrefs
    public void SaveName(){
        PlayerPrefs.SetString("name", txtName.text);  // Save the name entered in the input field
        userName = PlayerPrefs.GetString("name", "");  // Retrieve the saved name
        uiController.GetNameAndDisplay(userName);  // Display the saved name on the UI
        uiController.UserHasName();  // Inform the UI that the user has now set a name
    }
}