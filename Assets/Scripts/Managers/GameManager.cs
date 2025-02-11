using UnityEngine;

public class GameManager : MonoBehaviour
{
    int hp;
    public int HP{
        get { return hp; }
        set { hp = value; }
    }
    int numberToGuess;
    int numberLimit;
    int score;
    public int NumberLimit{
        get { return numberLimit; }
        set { numberLimit = value; }
    }
    [SerializeField]
    UIGame uiController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
