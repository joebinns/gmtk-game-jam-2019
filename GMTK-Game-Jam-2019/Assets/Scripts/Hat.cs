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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);

        if (collision.tag != "Player")
        {
            Debug.Log("collision");

            if (cameraFollowPlayer.returning == false)
            {
                cameraFollowPlayer.returning = true;
                cameraFollowPlayer.startTime = Time.time;

                cameraFollowPlayer.target = cameraFollowPlayer.hat.transform.position;
            }
            else
            {
                // DO NOTHING
            }
        }
    }
}
