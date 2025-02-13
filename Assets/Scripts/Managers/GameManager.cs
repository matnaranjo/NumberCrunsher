using UnityEngine;

public class GameManager : MonoBehaviour
{
    int hp;
    public int HP{
        get { return hp; }
        set { hp = value; }
    }
    int numberLimit;
    public int NumberLimit{
        get { return numberLimit; }
        set { numberLimit = value; }
    }
    int trackNum;
    public int TrackNum{
        get { return trackNum; }
        set { trackNum = value; }
    }
    int numberToGuess;
    int score;
    
    [SerializeField]
    UIGame uiController;
    [SerializeField]
    GameObject track;
    [SerializeField]
    GameObject canvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("canvas");
    }

    // Update is called once per frame
    void Update()
    {
    }

}
