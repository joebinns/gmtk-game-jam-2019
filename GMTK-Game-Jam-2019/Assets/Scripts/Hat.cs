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
