using UnityEngine;
using System.Collections;

public class RunnerMovement : MonoBehaviour 
{
    public Rigidbody controller;

    Vector3 jumpHeight = new Vector3(0,50,0);
    bool jump = false;
    bool grounded = false;
    float speed = 10;

	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Space) && !grounded)
        {
            jump = true;
        }


        if (jump)
        {
            controller.AddForce(jumpHeight,ForceMode.Impulse);
            jump = false;
        }
        else
        {
            controller.velocity =new Vector3(speed, 0, 0);
        }


	}

    public void SetGrounded(bool newValue)
    {
        grounded = newValue;
    }
}
