using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIIntro : MonoBehaviour
{
    [SerializeField] GameObject nameInput;
    [SerializeField] GameObject welcome;
    [SerializeField] TextMeshProUGUI nameDisplay;
    [SerializeField] GameObject clickToContinue;
    [SerializeField] GameObject theGenieText;
    [SerializeField] float timeForTheGenieToAppear = 19.3f;
    [SerializeField] float timeBetweenGenieAndInteractables = 19.3f;
    [SerializeField] Button start_;
    [SerializeField] TMP_InputField name;
    [SerializeField] Animator introMusic, fadeOut;
    [SerializeField] AudioSource evilLaugh;
    [SerializeField] AudioSource buttonClick; 

    private void Update()
    {
        if (name.text == "")
        {
            start_.interactable = false;
        }
        else
        {
            start_.interactable = true;
        }
    }

    public void UserHasNameInstantaneously()
    {
        nameInput.SetActive(false);
        welcome.SetActive(true);
        clickToContinue.SetActive(true);
    }


    public void UserHasName()
    {
        StartCoroutine(DelayedUIChange(true));
    }

    public void UserHasNoName()
    {
        StartCoroutine(DelayedUIChange(false));
    }

    private IEnumerator DelayedUIChange(bool hasName)
    {
        yield return new WaitForSeconds(timeForTheGenieToAppear);

        theGenieText.SetActive(true);

        yield return new WaitForSeconds(timeBetweenGenieAndInteractables); // Wait 

        if (hasName)
        {
            nameInput.SetActive(false);
            welcome.SetActive(true);
            clickToContinue.SetActive(true);
        }
        else
        {
            nameInput.SetActive(true);
            welcome.SetActive(false);
        }
    }

    public void GetNameAndDisplay(string name)
    {
        nameDisplay.text = nameDisplay.text + name;
    }

    public void TransitionIntoGame()
    {
        introMusic.Play("MusicOff");
        fadeOut.Play("fadeout");
        evilLaugh.Play();
    }

    public void ClickButton()
    {
        buttonClick.Play();
    }
}
