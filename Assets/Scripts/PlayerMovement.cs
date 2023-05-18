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
    bool canTeleport = true;
    bool inAir = false;
    
    [SerializeField] float playerSpeed = 10;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float jumpDelay = 1f;
    [SerializeField] float teleportDelay = 1.6f;
    [SerializeField] AudioClip SpawnSFX;
    [SerializeField] AudioClip GoalSFX; 
   
    

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<PolygonCollider2D>();
        myTransform = GetComponent<Transform>();
        // GetComponent<AudioSource>().Play();
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
            GetComponent<AudioSource>().Play();
            Debug.Log("Goal Reached");
            inPlay = false;
            myAnimator.SetBool("isRunning", false);
            Invoke("ChangeScene", 5f);
        }    
    }
    void OnSpecial(InputValue value)
    {
        Teleport(0,3,0);
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
    void Teleport(float x, float y, float z)
    {
        if (!canTeleport) {return;}
        else
        {
            Vector3 translation = new Vector3(x, y, z);
            myTransform.Translate(translation, Space.Self);
            canTeleport = false;
            Invoke("RechargeTeleport", teleportDelay);
        }
        
    }
    void RechargeTeleport()
    {
        canTeleport = true;
    }
    void ChangeScene() 
    {
        SceneManager.LoadScene(1);
    }
}
