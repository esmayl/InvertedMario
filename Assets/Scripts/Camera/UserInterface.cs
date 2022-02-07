using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserInterface : MonoBehaviour 
{
    public static UserInterface instance;
    public Transform gameEndScreen;
    public Text scoreText;
    public Image hpBar;
    int score;
    int hp;
    bool endReached = false;

    void Awake()
    {
        instance = this;
    }

    void Update () 
    {
        scoreText.text = ""+score;
        
        hpBar.transform.localScale = new Vector3((float)hp / 100,1,1);

        if (endReached)
        {
            ShowEndLevel();
        }
    }

    public void SetScore(int newScore)
    {
        score = newScore;
    }

    public void SetHp(int newHP)
    {
        hp = newHP;
    }

    public void SetEndReached()
    {
        endReached = true;
    }

    public void ShowEndLevel()
    {
        gameEndScreen.gameObject.SetActive(true);
    }
}
