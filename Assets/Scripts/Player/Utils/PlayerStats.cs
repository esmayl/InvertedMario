using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Animator animator;
    public AudioClip jumpClip;
    public AudioClip hitClip;
    public AudioClip gameOverClip;
    public string[] sourceNames;

    int hp = 100;
    int currentHp = 0;
    float lastHitTime = 0;
    float hitTimer = 0.5f;

    static int playerScore
    {
        get { return _playerScore; }
        set
        {
            _playerScore = value;
            UserInterface.instance.SetScore(_playerScore);
        }
    }

    //internal representation
    static int _playerScore;

    void OnEnable()
    {
        currentHp = hp;
    }


    public void Update()
    {
        animator.SetFloat("Speed", Input.GetAxisRaw("Horizontal"));
        UserInterface.instance.SetHp(currentHp);
        lastHitTime += Time.deltaTime;
    }

    public static void AddScore(int score)
    {
       playerScore += score;
    }

    public static void RemoveScore(int score)
    {
       playerScore -= score;
    }

    public static int GetScore()
    {
        return playerScore;
    }

    public void AddHp(int hpToAdd)
    {
        currentHp += hpToAdd;
        UserInterface.instance.SetHp(currentHp);
    }

    public void RemoveHp(int hpToRemove)
    {
        if (lastHitTime < hitTimer) { return; }
        if (currentHp > 0)
        {
            currentHp -= hpToRemove;
            
            animator.SetTrigger("Hit");

            lastHitTime = 0;
            UserInterface.instance.SetHp(currentHp);
            PlayAudio(sourceNames[0],hitClip);
        }
        if (currentHp <= 0)
        {
            UserInterface.instance.SetHp(currentHp);
            animator.SetTrigger("Hit");

            PlayDeathAudio();
            GetComponent<PlayerMovement>().Die();
        }
    }

    public void PlayAudio(string sName,AudioClip clip)
    {
        AudioSource tempSource = AudioMixClass.mixReff.GetSource(sName);
        tempSource.clip = clip;
        tempSource.Play();
    }

    public void PlayDeathAudio()
    {
        AudioSource tempSource = AudioMixClass.mixReff.GetSource(sourceNames[0]);
        tempSource.clip = gameOverClip;
        tempSource.Play();
    }
}
