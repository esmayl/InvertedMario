using UnityEngine;
using System.Collections;
using System;

enum EnemyState
{
    Patrolling,
    Chasing,
    LockedOn
}

public class EnemyMovement : Movement
{
    public string audioSourceName;
    public EnemyStats statsReff;

    EnemyState currentState = EnemyState.Patrolling;
    Transform player;
    float detectionRange = 6f;
    float speed = 600f;
    float closestRange = 0.5f;
    float walkDirection = -1f;
    float jumpHeight = 200f;
    float jumpCounter = 0;

	void Start ()
	{

	    statsReff = GetComponent<EnemyStats>();

	    currentState = EnemyState.Patrolling;
	    StartCoroutine("GroundCheck");
	    StartCoroutine("FindPlayer");
    }

	public override void FixedUpdate()
	{
	    base.FixedUpdate();

        jumpCounter -= Time.fixedDeltaTime;

	    switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Chasing:
                if(player)
                {
                    MoveToPlayer();
                }
                break;
        }
	}

    void OnDrawGizmosSelected()
    {
        Color gizmoColor = Color.blue;
        gizmoColor.a = 0.4f;
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, detectionRange);
    }

    void OnDestroy()
    {
        GetComponent<EnemyStats>().AddScoreToPlayer();
    }

    IEnumerator FindPlayer()
    {
        while(true)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange);

            if (hits.Length > 0 && player == null)
            {
                foreach (Collider2D c in hits)
                {
                    if (LayerMask.NameToLayer("Player") == c.gameObject.layer)
                    {
                        player = c.transform;
                        currentState = EnemyState.Chasing;
                    }
                }
            }

            if (player)
            {
                Vector3 dir = player.position - transform.position;
                dir.y = 0;

                if (dir.magnitude > closestRange)
                {
                    LookDirection(dir);
                }


                if (Vector3.Distance(player.position,transform.position) > detectionRange)
                {
                    player = null;
                    currentState = EnemyState.Patrolling;

                    //set new patrol
                    walkDirection = dir.normalized.x;
                }
            }

            //Keep here for checking loops, going to be important with lots of enemies 
            //Debug.Log("Enemy "+transform.name+" Loop");
            yield return new WaitForSeconds(0.1f);
        }
    }

    void JumpOnPlayer()
    {
        if (jumpCounter <0)
        {
            if (grounded  && !jumping || nearEdge && !jumping)
            {
                Jump(jumpHeight);
                jumpCounter = UnityEngine.Random.Range(1f,2f);
                PlayAudio(statsReff.jumpClip);
            }
        }
    }

    void Patrol()
    {
        if (nearEdge)
        {
            LookDirection(-transform.forward); 
            nearEdge = false; 
            walkDirection *= -1f;
        }
        if (grounded)
        {
            Move(speed, walkDirection);

            if (Physics2D.Raycast(transform.position, transform.forward, 1f, layers))
            {
                LookDirection(-transform.forward);
                walkDirection *= -1f;
            }
        }
    }

    void MoveToPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0;

        if (!jumping && dir.magnitude < detectionRange / 2) { JumpOnPlayer();}

        if (dir.magnitude < closestRange) return;

        Vector3 direction = player.position-transform.position;
        if (!jumping)
        {
            if (direction.x > 0.1f)
            {
                if (grounded || direction.y < -0.1f)
                {
                    walkDirection = 1f;
                    Move(speed, walkDirection);
                }
            }
            else
            {
                if (grounded || direction.y < -0.1f)
                {
                    walkDirection = -1f;
                    Move(speed, walkDirection);
                }
            }
        }

        if(jumping)
        {
            Move(speed, walkDirection);
        }
    }

    public void JumpOff()
    {
        for (int i = 0; i < 10; i++)
        {
            Move(speed, walkDirection);
        }

        Jump(jumpHeight);
        PlayAudio(statsReff.jumpClip);
    }

    void PlayAudio(AudioClip audioClip)
    {
        if (AudioMixClass.mixReff.GetSource(audioSourceName))
        {
            AudioSource tempAudio = AudioMixClass.mixReff.GetSource(audioSourceName);
            tempAudio.clip = audioClip;
            tempAudio.Play();
        }
    }
}
