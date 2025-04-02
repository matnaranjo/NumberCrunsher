using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class UIGame : MonoBehaviour
{
    [SerializeField]
    GameObject selectLevel;
    [SerializeField]
    GameObject easyScreen;
    [SerializeField]
    GameObject moderateScreen;
    [SerializeField]
    GameObject DifficultScreen;
    [SerializeField]
    GameObject defeat;
    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    GameObject continueNextLevel;
    [SerializeField]
    GameObject uiGeneral;
    [SerializeField]
    TextMeshProUGUI score;
    [SerializeField]
    TextMeshProUGUI maxScore;
    [SerializeField] AudioSource buttonClick;
    [SerializeField] Animator genieAndBall;

    public void LevelSelected(int level)
    { 
        selectLevel.SetActive(false);
        genieAndBall.Play("GenioGetIn");
        StartCoroutine(LevelDelay(level));
    }

    private IEnumerator LevelDelay(int level)
    {
        yield return new WaitForSeconds(5f);

        uiGeneral.SetActive(true);
        switch (level)
        {
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


    public void PlayerWon(){
        continueNextLevel.SetActive(true);
    }

    public void PlayerLost(){
        TurnOFOnGame();

        defeat.SetActive(true);
    }

    public void PlayerReset(){
        TurnOFOnGame();
        uiGeneral.SetActive(false);
        pauseMenu.SetActive(false);
        selectLevel.SetActive(true);
    }

    public void PlayerPaused(){
        pauseMenu.SetActive(true);
    }
    public void PlayerUnpaused(){
        pauseMenu.SetActive(false);
    }

    public void PlayerContinued(){
        continueNextLevel.SetActive(false);
    }

    public void SetScoreText(int score){
        this.score.text = $"Score: {score}";
    }

    public void SetMaxScoreText(int maxScore){
        this.maxScore.text = $"Max score: {maxScore}";
    }

    private void TurnOFOnGame(){
        bool onOff = false;
        easyScreen.SetActive(onOff);
        moderateScreen.SetActive(onOff);
        DifficultScreen.SetActive(onOff);
    }

    public void PlayerExit() {

          // Quit the game
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Stop play mode
        #else
        Application.Quit();  // Quit the game
        #endif
    }

    public void ClickButton()
    {
        buttonClick.Play();
    }
}   
