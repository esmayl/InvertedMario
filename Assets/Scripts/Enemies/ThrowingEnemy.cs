using UnityEngine;
using System.Collections;

public class ThrowingEnemy : Movement
{
    public GameObject BombGameObject;
    public Transform HandTransform;
    public string audioSourceName;

    EnemyState currentState = EnemyState.Patrolling;
    Transform player;
    EnemyStats statsRefference;
    float detectionRange = 6f;
    float speed = 600f;
    float closestRange = 0.5f;
    float walkDirection = -1f;
    float throwCounter = 0;
    float throwStrenght = 40f;

	void Start ()
	{
	    statsRefference = GetComponent<EnemyStats>();

        currentState = EnemyState.Patrolling;
	    StartCoroutine("GroundCheck");
	    StartCoroutine("FindPlayer");
    }
	
	public override void FixedUpdate () 
    {
	    base.FixedUpdate();

	    throwCounter -= Time.fixedDeltaTime;

	    switch (currentState)
	    {
	            case EnemyState.Patrolling:
                            Patrol();
                            break;
                case EnemyState.LockedOn:
	                        ThrowBomb();
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

    private void ThrowBomb()
    {
        if (throwCounter <0)
        {
            Vector3 instancePos = HandTransform.position;
            instancePos += transform.forward/10;
            instancePos += transform.up/6;    
            instancePos.z = 0;

            GameObject ga = (GameObject)Instantiate(BombGameObject, instancePos, Quaternion.identity);

            ga.GetComponent<Rigidbody2D>().AddForce((HandTransform.forward + HandTransform.up).normalized*throwStrenght, ForceMode2D.Impulse);

            ga.GetComponent<BombProjectile>().damage = statsRefference.enemyDamage;
                
            throwCounter = Random.Range(0.5f, 1.1f);
            statsRefference.animator.SetTrigger("Throw");
        }
    }

    IEnumerator FindPlayer()
    {
        while (true)
        {

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange);

            if (hits.Length > 0 && player == null)
            {
                foreach (Collider2D c in hits)
                {
                    if (LayerMask.NameToLayer("Player") == c.gameObject.layer)
                    {
                        player = c.transform;
                        currentState = EnemyState.LockedOn;
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

    void Patrol()
    {
        if (nearEdge)
        {
            LookDirection(-transform.forward);
            nearEdge = false;
            walkDirection *= -1f;
        }
        if (grounded) { Move(speed, walkDirection); }
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
