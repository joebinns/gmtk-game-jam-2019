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
    }

    /*
    void FollowPlayer()
    {
        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 10f);
        this.transform.position = newPos;
    }
    */

    void RestrictMousePos()
    {

    }

    void lookAhead()
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
