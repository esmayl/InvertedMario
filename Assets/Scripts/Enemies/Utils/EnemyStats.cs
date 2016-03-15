using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour 
{
    public int enemyDamage = 20;
    public int enemyValue;
    public AudioClip jumpClip;
    public AudioClip deathClip;
    public Animator animator;

    public void AddScoreToPlayer()
    {
        PlayerStats.AddScore(enemyValue);
    }

    public void Death()
    {
        animator.SetTrigger("Hit");
        GetComponentInChildren<Collider2D>().enabled = false;
        Destroy(gameObject,0.25f);
        SendMessage("PlayAudio",deathClip);
    }
}
