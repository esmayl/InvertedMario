using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour 
{

    public LayerMask layers;
    public bool canMove = true;

    internal Rigidbody2D body;
    internal bool grounded;
    internal bool jumping = false;
    internal bool nearEdge = false;
    
    float groundRadius = 0.05f;
    Transform groundChecker;

    void Awake()
    {
        if (GetComponent<Rigidbody2D>()) { body = this.GetComponent<Rigidbody2D>(); }
        else { Debug.LogError("No Rigidbody2D attached, de-activating."); this.enabled = false; }

        groundChecker = transform.Find("groundCheck");
    }

    public virtual void FixedUpdate()
    {
        Vector2 velocity = new Vector2(-body.velocity.x * 130f, 0);
        body.AddForce(velocity, ForceMode2D.Force);
    }
	
    public void Move(float speed,float direction)
    {
        if (!canMove) { return; }

        float newSpeed;

        newSpeed = speed - body.velocity.x;
        newSpeed = Mathf.Abs(newSpeed);

        Vector2 velocity = new Vector2(newSpeed*direction, body.velocity.y);
        if (body.isKinematic)
        {
            body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
        }
        else
        {
            body.AddForce(velocity, ForceMode2D.Force);
        }

    }
    
    public void Jump(float jumpHeight)
    {
        if (!canMove) { return; }

        body.velocity = new Vector2(body.velocity.x, 0);
        body.AddForce(new Vector2(body.velocity.x, jumpHeight),ForceMode2D.Impulse);
        jumping = true;
    }

    IEnumerator GroundCheck()
    {
        if (!canMove) { yield return null; }

        while (true)
        {
            Collider2D hit = Physics2D.OverlapCircle(groundChecker.position, groundRadius, layers);

            if (hit != null)
            {
                grounded = true;
            }
            else
            {
                grounded = false;
                if (Physics2D.Raycast(transform.position, -transform.up, 10f, layers))
                {
                    nearEdge = true;
                }
                else
                {
                    nearEdge = false;
                }
            }

            if (grounded) 
            {
                jumping = false;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    public void LookDirection(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    void OnDrawGizmosSelected()
    {
        Color gizmoColor = Color.red;
        gizmoColor.a = 0.5f;
        Gizmos.color = gizmoColor;
        if (groundChecker)
            Gizmos.DrawSphere(groundChecker.position, groundRadius);

    }
}
