using UnityEngine;
using System.Collections;

public class PlayerMovement : Movement
{
    float speed = 600f;
    float newSpeed;
    float jumpHeight = 220f;
    float counter = 0;
    float cooldown = 0.65f;
    Vector3 spherePos;

    PlayerStats playerStatsRefference;

    void Start()
    {
        playerStatsRefference = GetComponent<PlayerStats>();

        StartCoroutine("GroundCheck");
        counter = cooldown;
    }

    void Update()
    {
        if (counter >= cooldown)
        {
            if (Input.GetKeyDown(KeyCode.Space) && grounded && !jumping)
            {
                playerStatsRefference.animator.SetTrigger("Jump");
                playerStatsRefference.PlayAudio(playerStatsRefference.sourceNames[1],playerStatsRefference.jumpClip);
                Jump(jumpHeight);
                jumping = true;
                grounded = false;
                counter = 0;
            }
        }
        else
        {
            counter += Time.deltaTime;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            Move(speed,1f);
        }


        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            Move(speed,-1f);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.transform.tag == "MovingPlatform")
        {
            transform.parent = col.transform;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        transform.parent = null;
    }
}