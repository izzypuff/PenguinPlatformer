using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement Vars")]
    [SerializeField]
    //horizontal movement
    float horizontalMove;
    public float speed = 3f;
    //bool to not move
    public bool frozen = false;
    //jumping
    public bool grounded = false;
    public float castDist = 0.2f;
    public float jumpLimit = 2f;
    public float gravityScale = 2f;
    public float gravityFall = 40f;
    public bool jump = false;
    public int jumpNumber = 1;
    public AudioSource soundPlayer;

    [Header("Components")]
    //rigid body
    public Rigidbody2D myBody;

    //animator
    Animator myAnim;

    //sprite renderer
    SpriteRenderer myRend;

    [Header("Reset Transforms")]
    [SerializeField]
    Transform resetPos, bottomBounds;

    //vars for juice events
    JuiceEvents juiceEvents;

    //for juice events
    bool landed = false;
    public static bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        //gets components
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myRend = GetComponent<SpriteRenderer>();
        juiceEvents = GetComponent<JuiceEvents>();
    }

    // Update is called once per frame
    void Update()
    {
        //if not dead
        if (!dead)
        {
            //check for h input and v input
            CheckHInput();
            CheckVInput();
        }
    }

    //checks if pressing LR arrow keys and sets movement var
    void CheckHInput()
    {
        //horizontal movement (A&D, left&right)
        horizontalMove = Input.GetAxis("Horizontal");

        if (frozen)
        {
            horizontalMove = 0;
        }

        //if horizontal movement is happening to the right
        if (horizontalMove > 0.1f)
        {
            //walking animation boolean is true
            myAnim.SetBool("Walking", true);
            //do not flip the x to face right
            myRend.flipX = false;
        }
        //if horizontal movement is happening to the left 
        else if(horizontalMove < -0.1f)
        {
            //walking animation boolean is true
            myAnim.SetBool("Walking", true);
            //flip the x to face left
            myRend.flipX = true;
        }
        //if not moving
        else
        {
            //walking animation boolean is false
            myAnim.SetBool("Walking", false);
        }
    }
    //checks if pressing space and sets movement
    void CheckVInput()
    {
        //if jump button (space)
        if (Input.GetButtonDown("Jump"))
        {
            //if grounded or double jump is true
            if (grounded)
            {
                //jump is true
                jump = true;
                //play jump sound
                soundPlayer.Play();
                //animation jumping boolean is true
                myAnim.SetBool("Jumping", true);
            }

            if (jumpNumber >= 1)
            {
                //can double jump
                jump = true;
                //play jump sound
                soundPlayer.Play();
                //animation jumping boolean is true
                myAnim.SetBool("Jumping", true);
            }
        }
        else if (jumpNumber <= 0)
        {
            jump = false;
            //if not jumping, then jumping animation boolean is false
            myAnim.SetBool("Jumping", false);
        }
    }

    //check if falled out of bounds
    public void StartReset(CinemachineImpulseSource source)
    {
        //stop moving
        StopPhysics();
        //player is dead
        dead = true;
        //start fall death juice
        juiceEvents.FallDieJuiceStart(source);
    }

    //stop moving
    public void StopPhysics()
    {
        //normal grav
        myBody.gravityScale = 1f;
        //no movement
        myBody.velocity = new Vector3(0f, 0f, 0f);
    }

    //reset position
    public void ResetPos()
    {
        //not dead
        dead = false;
        //set current pos to set reset pos
        transform.position = resetPos.position;
    }

    //horizontal movement
    float HMove()
    {
        juiceEvents.HMoveJuice(horizontalMove);
        return horizontalMove * speed;
    }

    void FixedUpdate()
    {
        //if not dead
        if (!dead)
        {
            //set movement speed to horizontal movement based on key
            float moveSpeed = HMove();
            //controls what happens during jump
            StartJump();
            //set vertical movement to if jump is pressed
            VMove();
            //checks if on ground
            GroundCheck();

            //set velocity to set velocity
            myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);
        }
    }

    //checks if character is jumping or falling and sets gravity
    void VMove()
    {
        //is character is jumping
        if (myBody.velocity.y >= 0)
        {
            //put the current grav scale to set grav scale
            myBody.gravityScale = gravityScale;
        }
        //if character is falling
        else if (myBody.velocity.y < 0)
        {
            //put the current grav scale to fall grav scale
            myBody.gravityScale = gravityFall;
        }
    }

    //if jump is activated
    void StartJump()
    {
        //if jumping
        if (jump)
        {
            juiceEvents.JumpJuice(horizontalMove);
            //add force
            myBody.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            jumpNumber--;
            //stop jumping
            jump = false;
            //not landed
            landed = false;
        }
    }

    //checks if player is on ground
    void GroundCheck()
    {
        //create raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        Debug.DrawRay(transform.position, Vector2.down * castDist, new Color(255, 0, 0));
        //if touching ground obj
        if (hit.collider != null && hit.transform.name == "Ground")
        {
            myAnim.SetBool("Jumping", false);
            jumpNumber = 1;
            //if player is not on ground or landed
            if (!landed && !grounded)
            {
                //creates land particles
                juiceEvents.LandJuice(horizontalMove);
                //now player is landed
                landed = true;
            }
            //grounded is true
            grounded = true;
        }
        else
        {
            //if not touching ground obj, grounded is false
            grounded = false;
        }
    }
    
}
