using TMPro;
using UnityEngine;

public class UIIntro : MonoBehaviour
{
    [SerializeField]
    GameObject nameInput;
    [SerializeField]
    GameObject welcome;
    [SerializeField]
    TextMeshProUGUI nameDisplay;
    [SerializeField]
    GameObject clickToContinue;


    public void UserHasName(){
        nameInput.SetActive(false);
        welcome.SetActive(true);
        clickToContinue.SetActive(true);
    }

    public void UserHasNoName(){
        nameInput.SetActive(true);
        welcome.SetActive(false);
    }

    public void GetNameAndDisplay(string name){
        nameDisplay.text = nameDisplay.text + name;
    }
}
