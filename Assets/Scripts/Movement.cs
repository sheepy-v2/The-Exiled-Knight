using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Purchasing;

public class Movement : MonoBehaviour
{
    [SerializeField] private Vector2 movement;
    [SerializeField] private float speed;
    [SerializeField] private float rollSpeed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float wallSlideMultiplier;
    float hf = 0.0f;
    float vf = 0.0f;
    private bool grounded = true;
    private bool flipped = true;

    [SerializeField] private GameObject checkObject;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private float checkLength;
    [SerializeField] private float checkOffset;
    [SerializeField] private float maxSlopeAngle;
    [SerializeField] private float wallCheckOffset;
    [SerializeField] private bool wallchecked;
    [SerializeField] private LayerMask floor;

    Vector2 rayOriginCenter;
    Vector2 rayOriginLeft;
    Vector2 rayOriginRight;
    Vector2 rayOriginMiddle;
    Vector2 rayOriginBottom;
    Vector2 rayOriginTop;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        boxCollider = this.GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        speedMultiplier = speed;
    }
    void Update()
    {

        wallchecked = wallCheck();

        rayOriginLeft = new Vector2(checkObject.transform.position.x - boxCollider.size.x / 2, checkObject.transform.position.y - boxCollider.size.y / 2);
        rayOriginRight = new Vector2(checkObject.transform.position.x + boxCollider.size.x / 2, checkObject.transform.position.y - boxCollider.size.y / 2);
        grounded = IsGrounded();
        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (grounded || anim.GetBool("isWallSliding") == true)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            }
        }
        //dodgeroll
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.X))
        {
            if (grounded)
            {
                anim.SetBool("dodging", true);
                speedMultiplier = speed + rollSpeed;
            }
        }
        //movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        hf = movement.x > 0.01f ? movement.x : movement.x < -0.01f ? 1 : 0;
        vf = movement.y > 0.01f ? movement.y : movement.y == 0.00f ? movement.y < -0.0f ? 1 : 0 : -1;
        //check if character should be turned
        if (movement.x < -0.01f && flipped)
        {
            flip();
        }
        if (movement.x > 0.01f && !flipped)
        {
            flip();
        }
        anim.SetFloat("horizontal", hf);
        anim.SetBool("grounded", IsGrounded());
        anim.SetFloat("vertical", vf);
        anim.SetFloat("speed", vf);


        
        GetComponent<Rigidbody2D>().velocity = new Vector2(movement.x * speedMultiplier, GetComponent<Rigidbody2D>().velocity.y);

        if (!grounded && wallCheck())
        {
            // Prevent further horizontal movement towards the wall
            if (rb.velocity.y < 0)
            {
                anim.SetBool("isWallSliding", true);
                rb.velocity = new Vector2(0, rb.velocity.y * wallSlideMultiplier);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        if (wallCheck() == false)
        {
            anim.SetBool("isWallSliding", false);
        }

    }

    private void flip()
    {
        Vector3 currentscale = gameObject.transform.localScale;
        currentscale.x *= -1;
        gameObject.transform.localScale = currentscale;

        flipped = !flipped;
    }
    internal void DodgeEnd()
    {
        speedMultiplier = speed;
        anim.SetBool("dodging", false);
    }
    private bool wallCheck()
    {
        // Adjust the direction based on player movement (-1 for left, 1 for right)
        float direction = flipped == true ? 1f : -1f;

        Vector2 boxCenter = new Vector2(checkObject.transform.position.x + direction * (boxCollider.size.x / 2 + wallCheckOffset), transform.position.y);
        Vector2 boxSize = new Vector2(checkLength, boxCollider.size.y);
        Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0, floor);
        Debug.Log(hit);
        return hit != null;
    }
    private bool IsGrounded()
    {
        Debug.Log($"{boxCollider}");
        //RaycastHit2D hitCenter = Physics2D.Raycast(rayOriginCenter - new Vector2(0, checkOffset), Vector2.down, checkLength);
        RaycastHit2D hitLeft = Physics2D.Raycast(rayOriginLeft - new Vector2(0, checkOffset), Vector2.down, checkLength, floor);
        RaycastHit2D hitRight = Physics2D.Raycast(rayOriginRight - new Vector2(0, checkOffset), Vector2.down, checkLength, floor);
        Debug.Log($"{hitLeft}, {hitRight}");
        // If any of these rays hit, consider the character grounded
        return hitLeft.collider != null || hitRight.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rayOriginCenter - new Vector2(0, checkOffset), rayOriginCenter + Vector2.down * checkLength);
        Gizmos.DrawLine(rayOriginLeft - new Vector2(0, checkOffset), rayOriginLeft + Vector2.down * checkLength);
        Gizmos.DrawLine(rayOriginRight - new Vector2(0, checkOffset), rayOriginRight + Vector2.down * checkLength);
        float direction = flipped == true ? 1f : -1f;

        Vector2 boxCenter = new Vector2(checkObject.transform.position.x + direction * (boxCollider.size.x / 2 + wallCheckOffset), checkObject.transform.position.y);
        Vector2 boxSize = new Vector2(checkLength, boxCollider.size.y);
        Gizmos.DrawCube(boxCenter, boxSize);
    }
}
