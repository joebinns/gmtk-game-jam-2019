using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour
{
    public CameraFollowPlayer cameraFollowPlayer;


    void Start()
    {

    }


    void Update()
    {

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cameraFollowPlayer.pickup();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // return
        if (collision.tag == "enemy")
        {
            cameraFollowPlayer.hatReturn();
        }

        // drop
        else if (collision.tag == "terrain")
        {
            cameraFollowPlayer.hatDrop();
        }
    }
}
