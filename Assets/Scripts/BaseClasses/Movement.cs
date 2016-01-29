using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {

    public LayerMask layers;

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

        groundChecker = transform.FindChild("groundCheck");
    }

    public virtual void FixedUpdate()
    {
        Vector2 velocity = new Vector2(-body.velocity.x * 110, 0);
        body.AddForce(velocity, ForceMode2D.Force);
    }
	
    public void Move(float speed,float direction)
    {
        float newSpeed;

        newSpeed = speed - body.velocity.x;
        newSpeed = Mathf.Max(0, newSpeed);

        Vector2 velocity = new Vector2(newSpeed*direction, body.velocity.y);
        body.AddForce(velocity,ForceMode2D.Force);
    }
    
    public void Jump(float jumpHeight)
    {
        body.velocity = new Vector2(body.velocity.x, 0);
        body.AddForce(new Vector2(body.velocity.x, jumpHeight),ForceMode2D.Impulse);
        jumping = true;
    }

    IEnumerator GroundCheck()
    {
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
                if (Physics2D.Raycast(transform.position, -transform.up, 10f, layers)) { nearEdge = true; }
            }

            if (grounded) 
            {
                jumping = false;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void LookDirection(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction, transform.up);
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
