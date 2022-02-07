using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour 
{
    public int enemyDamage = 20;
    public int enemyValue;
    public AudioClip jumpClip;
    public AudioClip deathClip;
    public Animator animator;
    
    void OnDisable()
    {
        animator.Rebind();
    }

    public void AddScoreToPlayer()
    {
        PlayerStats.AddScore(enemyValue);
    }

    public void Death()
    {
        animator.SetTrigger("Hit");
        Invoke("Disable",0.25f);
        SendMessage("PlayAudio",deathClip);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
