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


    void Start()
    {
        Cursor.visible = false;
        cam = Camera.main;
    }


    void LateUpdate()
    {
        if (followPlayer == true)
        {
            lookAhead();
        }

        if (isAttacking == true)
        { 
            float distCovered = (Time.time - startTime) * speed;

            float fracJourney = distCovered / journeyLength;
            fracJourney = Mathf.Min(fracJourney, 1);


            // update hat pos
            if (returning == true)
            {
                hat.transform.position = Vector3.Lerp(target, player.transform.position, fracJourney);  // return to the current player position
            }
            else
            {
                hat.transform.position = Vector3.Lerp(startPos, target, fracJourney);
            }


            if (fracJourney >= 1 && returning == false)
            {
                returning = true;
                startTime = Time.time;
            }
            else if (fracJourney >= 1 && returning == true)
            {
                isAttacking = false;
                hat.SetActive(false);
            }
        }
    }


    public void Attack()
    {
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
        if (dir.magnitude >= 1f)
        {
            cursor.transform.position = player.transform.position + (dir);
        }
        else
        {
            cursor.transform.position = player.transform.position + (dir.normalized * 1f);
        }
    }
}
