using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Camera cam;
    private bool followPlayer = true;

    private Vector3 mousePos;

    public GameObject cursor;

    public GameObject hat;


    [HideInInspector]
    public bool isAttacking;
    [HideInInspector]
    public Vector3 startPos;
    [HideInInspector]
    public Vector3 target;
    [HideInInspector]
    public float startTime;
    [HideInInspector]
    public float journeyLength;
    [HideInInspector]
    public float speed = 10f;
    [HideInInspector]
    public bool returning = false;
    [HideInInspector]
    public bool dropping = false;
    [HideInInspector]
    public bool hasHit = false;
    private float fracJourney;



    void Start()
    {
        Cursor.visible = false;
        cam = Camera.main;
    }

    public void hatReturn()
    {
        hasHit = true;

        if (returning == false)
        {
            returning = true;
            startTime = Time.time;

            target = hat.transform.position;
        }
        else
        {
            // DO NOTHING
        }
    }

    // TODO: MAKE IT BOUNCE A BIT, NOT JUST A SUDDEN STOP (LOOKS LIKE A BUG)
    //       THIS COULD BE ACHIEVED BY MAKING IT LERP A LESSER DISTANCE IN THE OPP DIRECTION
    public void hatDrop()
    {
        dropping = true;

        if (fracJourney >= 1)
        {
            // DO NOTHING
        }
        else
        {
            // bounce
            // opposite direction to current travel...
            if (returning == true)
            {
                returning = false;

                // ORDER MATTERS
                startPos = hat.transform.position;   // current position
                target = ((target - player.transform.position).normalized) + startPos;
            }
            else if (returning == false)
            {
                returning = true;

                // ORDER MATTERS
                target = ((startPos - target).normalized) + hat.transform.position;
                startPos = hat.transform.position;   // current position
            }
        }


        startTime = Time.time;
        journeyLength = Vector3.Distance(startPos, target) * (fracJourney * 10f);
        //journeyLength = Vector3.Distance(startPos, target) * 2; // *2, to make it slower

        // then stop attacking
        //isAttacking = false;
    }


    private void Update()
    {
        if (followPlayer == true)
        {
            lookAhead();
        }
    }

    void FixedUpdate()
    {
        if (isAttacking == true)
        {
            hat.GetComponent<Animator>().SetBool("isMoving", true);

            float distCovered = (Time.time - startTime) * speed;

            fracJourney = distCovered / journeyLength;
            fracJourney = Mathf.Min(fracJourney, 1);

            // vary the speed
            fracJourney = Mathf.Sqrt(fracJourney);

            Debug.Log(fracJourney);

            // update hat pos
            if (dropping == true)
            {

                //hat.transform.position = Vector3.Lerp(startPos, target, fracJourney);
                hat.GetComponent<Rigidbody2D>().MovePosition(Vector3.Lerp(startPos, target, fracJourney));
            }
            else if (returning == true)
            {
                //hat.transform.position = Vector2.Lerp(target, player.transform.position, fracJourney);
                hat.GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(target, player.transform.position, fracJourney));  // return to the current player position
            }
            else
            {
                //hat.transform.position = Vector2.Lerp(startPos, target, fracJourney);
                hat.GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(startPos, target, fracJourney));
            }

            if (fracJourney >= 1 && dropping == true)
            {
                isAttacking = false;
                hat.GetComponent<Animator>().SetBool("isMoving", false);
            }

            if (fracJourney >= 1 && returning == false)
            {
                returning = true;
                startTime = Time.time;
                if (hasHit == true)
                {
                    //journeyLength = Vector3.Distance(target, player.transform.position);
                }
                else
                {
                    isAttacking = false;
                    hat.GetComponent<Animator>().SetBool("isMoving", false);
                    //hatDrop();
                }
            }
            else if (fracJourney >= 1 && returning == true)
            {
                isAttacking = false;
                //hat.SetActive(false);
            }
        }
    }


    public void Attack()
    {
        dropping = false;

        hat.transform.position = player.transform.position;

        returning = false;

        hat.SetActive(true);

        startPos = player.transform.position;
        target = cursor.transform.position;

        isAttacking = true;

        startTime = Time.time;
        journeyLength = Vector3.Distance(startPos, target);
        // lerp hat position to the target over time
    }


    private void lookAhead()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector3 dir = mousePos - this.transform.position;       


        // graze stuff
        if (dir == Vector3.zero)
        {
            dir = new Vector3(1, 0, 0);
        }

        float scale = 2f;

        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 10f);

        transform.position = newPos += (dir.normalized * scale);


        // GUI Mouse cursor stuff      
        if (dir.magnitude <= 1f)
        {
            cursor.transform.position = player.transform.position + (dir.normalized * 1f);
        }
        else if (dir.magnitude > 6f)
        {
            cursor.transform.position = player.transform.position + (dir.normalized * 6f);
        }
        else
        {
            cursor.transform.position = player.transform.position + (dir);
        }
    }
}
