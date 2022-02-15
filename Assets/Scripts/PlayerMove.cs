using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveVector;
    public float speed = 2f;
    public Animator anim;
    public SpriteRenderer sr;
    public bool faceRight = true;
    public bool hit = true;

    void Start()
    {
        rb      = GetComponent<Rigidbody2D>();
        anim    = GetComponent<Animator>();
        sr      = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        walk();
        Reflect();
        Jump();
        CheckingGround();
        Hit();
    }

    // Ходить
    void walk()
    {
            moveVector.x = Input.GetAxis("Horizontal");
            moveVector.y = Input.GetAxisRaw("Vertical");
            anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
            anim.SetFloat("moveY", Mathf.Abs(moveVector.y));

            //rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y); 
            rb.velocity = new Vector2(moveVector.x * speed, moveVector.y * speed);
           // CameraFollow2D.faceLeft = true;
    }

    void Hit()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
           // anim.SetFloat(, Mathf.Abs(moveVector.x));
            anim.SetInteger("hit",1);

        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            // anim.SetFloat(, Mathf.Abs(moveVector.x));
            anim.SetInteger("hit", 0);
        }
    }


    // Повороты влевао \ вправо
    void Reflect()
    {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }

    public float jumpForce = 7f;
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) )
        {
            //&& onGround
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //rb.gravityScale = 1;
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    public bool onGround;
    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;

    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
        anim.SetBool("onGround", onGround);
    }


}