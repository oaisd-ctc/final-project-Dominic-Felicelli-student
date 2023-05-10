using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Collider2D myCollider;
    Transform myTransform;
    [SerializeField] BoxCollider2D endGoal;
    // [SerializeField] CanvasGroup Results;
    public bool inPlay = true;
    bool inAir = false;
    
    [SerializeField] float playerSpeed = 10;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float jumpDelay = 1f;
   
    

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<PolygonCollider2D>();
        myTransform = GetComponent<Transform>();
    }

    
    void Update()
    {
        if (inPlay)
        {
            Run();
            FlipSprite();
        }
        
    }
    void OnMove(InputValue value)
    {
        
        moveInput = value.Get<Vector2>();
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        if (moveInput.x != 0)
        {
            myAnimator.SetBool("isRunning", true);
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
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
        Jump(value, 1);
    }
    void EndJump()
    {
        inAir = false;
        myAnimator.SetBool("isJumping", false);
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.GetComponent<BoxCollider2D>() == endGoal)
        {
            Debug.Log("Goal Reached");
            inPlay = false;
            myAnimator.SetBool("isRunning", false);
            SceneManager.LoadScene(1);
        }    
    }
    void OnSpecial(InputValue value)
    {
        Jump(value, 10);
  
    }
    void Jump(InputValue value, int power)
    {
        if (inAir) {return;}
        if(value.isPressed)
        {
            if (myCollider.IsTouchingLayers())
            {
                myRigidbody.velocity += new Vector2 (0f, jumpSpeed * power);
                myAnimator.SetBool("isJumping", true);
                inAir = true;
                Invoke("EndJump", jumpDelay);               
            }

        }
    }
}
