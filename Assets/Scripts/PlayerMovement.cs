using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    // Animator myAnimator;
    Collider2D myCollider;
    Transform myTransform;
    bool isAlive = true;
    [SerializeField] float playerSpeed = 10;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float climbSpeed = 2f;
    [SerializeField] Vector2 deathKick = new Vector2 (-10f, 10f);
    

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        // myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<PolygonCollider2D>();
        myTransform = GetComponent<Transform>();
    }

    
    void Update()
    {
        if (!isAlive) {return;}
        Run();
        // FlipSprite();
    
    }
    void OnMove(InputValue value)
    {
        if (!isAlive) {return;}
        moveInput = value.Get<Vector2>();
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        if (moveInput.x != 0)
        {
            // myAnimator.SetBool("isRunning", true);
        }
        else
        {
            // myAnimator.SetBool("isRunning", false);
        }
    }
    void FlipSprite() 
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
    void OnJump(InputValue value)
    {
        if (!isAlive) {return;}
        if(value.isPressed)
        {
            if (myCollider.IsTouchingLayers())
            {
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);                
            }

        }
    }
    
    
}
