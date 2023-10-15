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

    //jumping
    bool grounded = false;
    public float castDist = 0.2f;
    public float jumpLimit = 2f;
    public float gravityScale = 2f;
    public float gravityFall = 40f;
    bool jump = false;
    bool doubleJump = false;
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
        if (!dead)
        {
            CheckHInput();
            CheckVInput();
        }
    }

    //checks if pressing LR arrow keys and sets movement var
    void CheckHInput()
    {
        //horizontal movement (A&D, left&right)
        horizontalMove = Input.GetAxis("Horizontal");
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
       
    void CheckVInput()
    {
        //if jump button (space)
        if (Input.GetButtonDown("Jump"))
        {
            //if grounded or double jump is true
            if (grounded || doubleJump)
            {
                //jump is true
                jump = true;
                //double jump is not true
                doubleJump = !doubleJump;
                //animation jumping boolean is true
                myAnim.SetBool("Jumping", true);
            }
        }
        else
        {
            //if not jumping, then jumping animation boolean is false
            myAnim.SetBool("Jumping", false);
        }
    }

    //check if falled out of bounds
    public void StartReset(CinemachineImpulseSource source)
    {
        StopPhysics();
        dead = true;
        juiceEvents.FallDieJuiceStart(source);
    }

    public void StopPhysics()
    {
        myBody.gravityScale = 1f;
        myBody.velocity = new Vector3(0f, 0f, 0f);
    }

    public void ResetPos()
    {
        dead = false;
        transform.position = resetPos.position;
    }

    float HMove()
    {
        juiceEvents.HMoveJuice(horizontalMove);
        return horizontalMove * speed;
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            float moveSpeed = HMove();

            StartJump();

            VMove();

            GroundCheck();

            //set velocity to set velocity
            myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);
        }
    }

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

    void StartJump()
    {
        //if jumping
        if (jump)
        {
            soundPlayer.Play();
            juiceEvents.JumpJuice(horizontalMove);
            //add force
            myBody.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            //stop jumping
            jump = false;
            //not landed
            landed = false;
        }
    }

    void GroundCheck()
    {
        //create raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        Debug.DrawRay(transform.position, Vector2.down * castDist, new Color(255, 0, 0));
        //if touching ground obj
        if (hit.collider != null && hit.transform.name == "Ground")
        {
            if (!landed && !grounded)
            {
                juiceEvents.LandJuice(horizontalMove);
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
