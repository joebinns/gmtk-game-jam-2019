using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ColliderSeek : MonoBehaviour
{
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.GetComponentInParent<AIDestinationSetter>().target = collision.GetComponent<Transform>();
        }
    }
}
