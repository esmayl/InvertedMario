using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : Movement
{
    public bool useMobileInput = false;
    MobileInput mobileInput;
    float speed = 700f;
    float newSpeed = 800f;
    float jumpHeight = 220f;
    float counter = 0;
    float cooldown = 0.55f;
    Vector3 spherePos;

    PlayerStats playerStatsRefference;

    void OnEnable()
    {
        playerStatsRefference = GetComponent<PlayerStats>();

        StartCoroutine("GroundCheck");
        counter = cooldown;

        if (useMobileInput)
        {
            try
            {
                mobileInput = GetComponent<MobileInput>();
            }
            catch (Exception e)
            {
                mobileInput = gameObject.AddComponent<MobileInput>();
            }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (useMobileInput)
        {
            return;
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            Move(speed, 1f);
        }


        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            Move(speed, -1f);
        }

        
    }

    public void Update()
    {

        counter += Time.deltaTime;

        if (mobileInput && Input.touches.Length> 0)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (mobileInput.CheckTouch(touch))
                {
                    case ActionType.MoveL:
                        Move(speed, -1f);
                        break;

                    case ActionType.MoveR:
                        Move(speed, 1f);
                        break;
                    
                    case ActionType.Jump:
                        if (counter >= cooldown && grounded && !jumping)
                        {
                            Jump(jumpHeight);

                            playerStatsRefference.animator.SetTrigger("Jump");
                            playerStatsRefference.PlayAudio(playerStatsRefference.sourceNames[1], playerStatsRefference.jumpClip);
                            Jump(jumpHeight);
                            jumping = true;
                            grounded = false;
                            counter = 0;
                        }
                        break;
                    
                }
            }
            return;
        }


        if (counter >= cooldown)
        {
            if (Input.GetKeyDown(KeyCode.Space) && grounded && !jumping)
            {
                playerStatsRefference.animator.SetTrigger("Jump");
                playerStatsRefference.PlayAudio(playerStatsRefference.sourceNames[1], playerStatsRefference.jumpClip);
                Jump(jumpHeight);
                jumping = true;
                grounded = false;
                counter = 0;
            }
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


    public void Die()
    {
        Invoke("Respawn", 0.5f);
        gameObject.SetActive(false);
    }

    void Respawn()
    {
        LevelManager.instance.ReloadLevel();
    }

    void OnDisable()
    {
        StopCoroutine("GroundCheck");
    }
}