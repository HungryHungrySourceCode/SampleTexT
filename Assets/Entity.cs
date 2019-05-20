using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{






    //Movement - splitting this up so you can move things around easier if you want to move code around to other scripts.
    public float inputX;
    public float inputY;
    public float inputJump;

    [Header("Attributes")]

    public int numberOfJumps = 1;
    public float speed;
    public float jumpPower;        //the overall power of the jump while being held.
    public float jumptime;         //the time a jump can add momentum upwards.
    public float jumpOverTimeRatio = 0.5f; //momentum added to the start of the jump. Momentum decreases over jump. The time you can hold jump and go up is your jump time.
    public float jumpInitialBurstStrength = 20f;  //initial strength of the jump.
    public float maxWindupJumpMultiplier;    //the multiplier for the power of the overall jump while wound up.
    public float windupJumpChargeRate = 1f; //rate the jump will charge while the key is held down on the ground.

    public float dashTime = 5f;

    private int currentJump = 1;    //the current jump, 1 is equal to zero.

    [Header("States")]
    public bool prepareJump;    //preparing a jump
    public bool jumping;        //actually jumping
    public bool dashing;
    public bool grippingSurface;   //currently ghetto as shit and just used on collider stay on collider exit. Unintentionally added wall climbing.

    public Rigidbody2D rb;

    [Header("Animation")]
    public Animator animController;
    public string movement_LeftString;
    public string movement_RightString;
    public string movement_UpString;
    public string movement_DownString;
    public string movement_Preparation_Jumpstring;



    public IEnumerator PlayerJumpWithWindUp(float _jumptime)
    {

        if (grippingSurface == false)
        {
            StartCoroutine(PlayerJump(jumptime));
            yield break;
        }


        float windupT = 1f;
        while (Input.GetKey(KeyCode.Space) == true)
        {
            windupT += Time.deltaTime * windupJumpChargeRate;
            Mathf.Clamp(windupT, 1f, maxWindupJumpMultiplier);
            prepareJump = true;
            if (Input.GetKeyUp(KeyCode.W))
                {
                prepareJump = false;
                }
            yield return new WaitForEndOfFrame();
        }





        if (grippingSurface == false && currentJump >= numberOfJumps)
        {
            prepareJump = false;
            yield break;
        }

        if (jumping == true)
        {
            if (grippingSurface == false && currentJump <= numberOfJumps)
            {
            }
            else
            {
                yield break;
            }
        }


        if (grippingSurface == false)
        {
            currentJump++;
        }
        if (grippingSurface == true)
        {
            currentJump = 1;
        }

        jumping = true;
        float t = jumptime;
        bool letgo = false;



        if (letgo == false && jumping == true)
        {
            rb.AddForce(Vector2.up * windupT * jumpPower * jumpOverTimeRatio * -jumpInitialBurstStrength / 3f );
            if (Input.GetKeyDown(KeyCode.S))
            {
                letgo = true;
            }
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            rb.AddForce(Vector2.up * jumpPower * jumpOverTimeRatio * jumpInitialBurstStrength * windupT);
        }

        if (rb.velocity.y <= -0.1f)
        {
            Vector2 correctVel = new Vector2(0f, -rb.velocity.y);
            rb.velocity += correctVel;
        }

        while (t > 0f)
        {
            prepareJump = false;
            t -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.S))
            {
                letgo = true;
            }

            if (letgo == true)
            {
                t = 0f;
                jumping = false;
                yield break;
            }

            if (inputJump <= 0f)
            {
                jumping = false;
            }

            rb.AddForce((Vector2.up * jumpPower) * inputJump * (jumpOverTimeRatio * ((t / jumptime)) * rb.gravityScale) * windupT * windupT);


            yield return new WaitForEndOfFrame();
        }
        prepareJump = false;
        yield break;
    }


    public IEnumerator PlayerJump(float _jumptime)
    {

        if (grippingSurface == false && currentJump >= numberOfJumps)
        {
            yield break;
        }

        if (jumping == true)
        {
            if (grippingSurface == false && currentJump <= numberOfJumps)
            {
            }
            else
            {
                yield break;
            }
        }


        if (grippingSurface == false)
        {
            currentJump++;
        }
        if (grippingSurface == true)
        {
            currentJump = 1;
        }

        jumping = true;
        float t = jumptime;
        bool letgo = false;



        if (letgo == false && jumping == true)
        {
            rb.AddForce(Vector2.up * jumpPower * jumpOverTimeRatio * -jumpInitialBurstStrength / 3f);
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
            {
                letgo = true;
            }
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            rb.AddForce(Vector2.up * jumpPower * jumpOverTimeRatio * jumpInitialBurstStrength);
        }

        if (rb.velocity.y <= -0.1f)
        {
            Vector2 correctVel = new Vector2(0f, -rb.velocity.y);
            rb.velocity += correctVel;
        }

        while (t > 0f)
        {

            t -= Time.deltaTime;

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
            {
                letgo = true;
            }

            if (letgo == true)
            {
                t = 0f;
                jumping = false;
                yield break;
            }

            if (inputJump <= 0f)
            {
                jumping = false;
            }

            rb.AddForce((Vector2.up * jumpPower) * inputJump * (jumpOverTimeRatio * ((t / jumptime)) * rb.gravityScale));


            yield return new WaitForEndOfFrame();
        }

        while (letgo == false)
        {
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
            {
                letgo = true;
                jumping = false;
                yield break;
            }
            else if (Input.GetKey(KeyCode.W) == false || Input.GetKey(KeyCode.Space) == false)
            {
                letgo = true;
                jumping = false;
                yield break;
            }

            if (grippingSurface == true)
            {
                jumping = false;
                currentJump = 1;
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }


    public IEnumerator PlayerDash(float startupTime)
    {
        //will finish at a later rate.
        if (dashing == true)
        {
            yield break;
        }

        dashing = true;
        float t = dashTime;

  
    }
    //Movement - splitting this up so you can move things around easier if you want to move code around to other scripts.



    private void Update()
    {

        if (prepareJump == true)
        {
            animController.SetBool(movement_Preparation_Jumpstring, true);
        }
        else
        {
            animController.SetBool(movement_Preparation_Jumpstring, false);
        }

        if (jumping == true)
        {
            animController.SetBool("Jumping", true);
        }
        else
        {
            animController.SetBool("Jumping", false);
        }

        if (jumping == true && currentJump >= 1)
        {
            animController.SetTrigger("AirJump");
        }

        if (Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.2f && jumping == false && prepareJump == false)
        {
            animController.SetBool("Moving", true);
        }
        else
        {
            animController.SetBool("Moving", false);
        }

        if (grippingSurface == true)
        {
            animController.SetBool("Grounded", true);
        }
        else
        {
            animController.SetBool("Grounded", false);
        }


        //Movement and temp input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PlayerJumpWithWindUp(jumptime));
        }

        rb.AddForce(Vector2.right * Input.GetAxisRaw("Horizontal") * speed);
        //Movement and temp input



    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        grippingSurface = true;
        //Debug.Log("I: The player. Am in contact with something. Now i must nut loudly!");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grippingSurface = false;
    }





    public Collider2D col;

    private void OnDrawGizmos()
    {
        RaycastHit2D[] rays = new RaycastHit2D[4];

        Gizmos.DrawRay(transform.position, rb.velocity);


    }


}
