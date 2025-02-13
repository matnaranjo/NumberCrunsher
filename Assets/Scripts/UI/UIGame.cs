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


    public void LevelSelected(int level){
        selectLevel.SetActive(false);
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

    }

    public void PlayerLost(){

    }

    public void PlayerPaused(){

    }

    public void PlayerContinued(){
        
    }


}
