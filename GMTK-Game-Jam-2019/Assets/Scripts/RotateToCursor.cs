using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCursor : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera cam;
    private Rigidbody2D _rb;

    private Animator animator;


    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        cam = Camera.main;

        animator = GetComponent<Animator>();
    }


    void Update()
    {
        RotateToCamera();
    }


    private void RotateToCamera()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - cam.transform.position.z));

        // rotation snap
        float angleDeg = Mathf.Atan2((mousePos.y - transform.position.y), (mousePos.x - transform.position.x)) * Mathf.Rad2Deg;
        if (45 <= angleDeg && angleDeg < 135)
        {
            angleDeg = 0;    // UP

            animator.SetInteger("lookDirection", 0);        // look direction: 0 = up, 1 = right, 2 = down, 3 = left
        }
        else if (-45 <= angleDeg && angleDeg < 45)
        {
            angleDeg = 90;    // RIGHT

            animator.SetInteger("lookDirection", 1);

            _rb.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (-135 <= angleDeg && angleDeg < -45)
        {
            angleDeg = 180;    // DOWN

            animator.SetInteger("lookDirection", 2);
        }
        else   //-135 <-> 135
        {
            angleDeg = 270;     // LEFT

            // flip rigidbody
            animator.SetInteger("lookDirection", 1);
            _rb.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        //_rb.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePos.y - transform.position.y), (mousePos.x - transform.position.x)) * Mathf.Rad2Deg);
        //_rb.transform.eulerAngles = new Vector3(0, 0, angleDeg);
    }
}
