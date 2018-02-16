using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public Rigidbody2D rb;

    public float speed;
    public float jumpVelocity;
    private float fallMultiplier;
    private float lowJumpMultiplier;

    private bool isGrounded;
    public float limit;
    private int jumps;

    private void Awake()
    {
        jumps = 0;
        rb = GetComponent<Rigidbody2D>();
        fallMultiplier = 2.5f;
        lowJumpMultiplier = 2f;
    }
    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space) && jumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(jumpVelocity * Vector2.up);
            jumps -= 1;
        }

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up*Physics2D.gravity.y*(fallMultiplier - 1)*Time.deltaTime;
        }

        if(rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        if (rb.velocity.x < limit * -1)
        {
            rb.velocity = new Vector2(limit * -1, rb.velocity.y);
        }
        else if (rb.velocity.x > limit)
        {
            rb.velocity = new Vector2(limit, rb.velocity.y);
        } else { 
            rb.velocity += movement*speed;
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            jumps = 2;
        }
        if(collision.gameObject.CompareTag("BlackHole"))
        {
            //gameover;
        }
    }
}

