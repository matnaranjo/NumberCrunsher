using TMPro;
using UnityEngine;

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


    public void LevelSelected(int level){
        selectLevel.SetActive(false);
        uiGeneral.SetActive(true);
        switch(level){
            case 1:
            easyScreen.SetActive(true);
            break;
            case 2:
            moderateScreen.SetActive(true);
            break ;
            case 3:
            DifficultScreen.SetActive(true);
            break;
        }
    }

    public void PlayerWon(){
        continueNextLevel.SetActive(true);
    }

    public void PlayerLost(){

    }

    public void PlayerPaused(){

    }

    public void PlayerContinued(){
        continueNextLevel.SetActive(false);
    }

    public void SetScoreText(int score){
        this.score.text = $"Score: {score}";
    }


}
