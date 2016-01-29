using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserInterface : MonoBehaviour {

    public Text scoreText;
    public Image hpBar;
    static int score;
    static int hp;

	void Update () 
    {
        scoreText.text = ""+score;
        
        hpBar.fillAmount = (float)hp / 100;
	}

    public static void SetScore(int newScore)
    {
        score = newScore;
    }

    public static void SetHp(int newHP)
    {
        hp = newHP;
    }
}
