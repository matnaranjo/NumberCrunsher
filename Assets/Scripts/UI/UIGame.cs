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
    GameObject difficultScreen;

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
    [SerializeField] TextMeshProUGUI range;
    [SerializeField] AudioSource buttonClick;
    [SerializeField] Animator genieAndBall;
    [SerializeField] Animator genieMovement;
    [SerializeField] Animator genie;
    [SerializeField] Animator ball;
    [SerializeField] Animator foguito;
    [SerializeField] Animator lightning;
    [SerializeField] AudioSource response;
    [SerializeField] AudioSource levelUp;
    [SerializeField] AudioClip wrong, right;

    [SerializeField] GameObject tracks;
    [SerializeField] GameManager gm;
    public void LevelSelected(int level)
    {
        selectLevel.SetActive(false);
        genieAndBall.Play("GenioGetIn");
        StartCoroutine(LevelDelay(level));
        range.text = $"(0 - {gm.CurrentNumberLimit})";
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
                difficultScreen.SetActive(true);
                break;
        }

    }


    public void PlayerWon()
    {
        StartCoroutine(LevelUpYay());
    }

    IEnumerator LevelUpYay()
    {
        yield return new WaitForSeconds(0.5f);

      //  tracks.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        lightning.Play("Lightning");
        genieMovement.Play("GenitoHit");
        levelUp.Play();
        
        yield return new WaitForSeconds(2f);
        continueNextLevel.SetActive(true);

      // tracks.SetActive(true);

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

    public void PlayerContinued()
    {
        continueNextLevel.SetActive(false);
    }

    public void SetScoreText(int score){
        this.score.text = $"Score: {score}";
    }

    public void SetMaxScoreText(int maxScore){
        this.maxScore.text = $"Max score: {maxScore}";
    }


    public void SetRangeText(int limit)
    {
        range.text = $"(0 - {limit})";
    }

    private void TurnOFOnGame(){
        bool onOff = false;
        easyScreen.SetActive(onOff);
        moderateScreen.SetActive(onOff);
        difficultScreen.SetActive(onOff);
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

    public void PlayerFailedTurn()
    {
        response.clip = wrong;
        genie.Play("TurnRed");
        ball.Play("TurnRed");
        foguito.Play("FoguitoShine");
        response.Play();
    }

    public void PlayerGotAtLeastOneRight()
    {
        response.clip = right;
        genie.Play("TurnGreen");
        ball.Play("GetGreenBall");
        foguito.Play("FoguitoOff" );
        response.Play();
    }
}   
