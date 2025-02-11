using UnityEngine;
using TMPro;

public class TrackController : MonoBehaviour
{
    
    TextMeshProUGUI guesses;
    TextMeshProUGUI guessResult;
    TMP_InputField userGuess;
    AudioController audios;
    GameManager gm;
    int hp;
    int range;



    void Start(){
        gm = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<GameManager>();
    }

    private void InitializeValues(){

    }

    private void HandleInput(){

    }

    
}
