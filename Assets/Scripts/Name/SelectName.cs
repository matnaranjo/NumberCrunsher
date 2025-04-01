using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectName : MonoBehaviour
{
    [SerializeField]
    UIIntro uiController;
    [SerializeField]
    TMP_InputField txtName;
    private string userName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // DEBUGGING CODE
        PlayerPrefs.DeleteAll();
        // DEBUGGING CODE

        userName = PlayerPrefs.GetString("name", "");
        if (userName == ""){
            uiController.UserHasNoName();
        }
        else{
            uiController.GetNameAndDisplay(userName);
            uiController.UserHasName();
        }
    }

    public void SaveName(){
        PlayerPrefs.SetString("name", txtName.text);
        userName = PlayerPrefs.GetString("name", "");
        uiController.GetNameAndDisplay(userName);
        uiController.UserHasNameInstantaneously();
    }
}
