using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _body;

    private Vector2 _inputs = Vector2.zero;


    private float _attack;
    private bool m_isAxisInUse = false;

    private bool _hasHat = true;


    //private Vector2 playerMove = Vector2.zero;

    private Animator animator;

    public float speed = 30/4f;

    public CameraFollowPlayer cameraFollowPlayer;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    void Update() // GET INPUTS & APPLY 'ONE-TIME' PHYSICS
    {
        animator.SetBool("hasHat", _hasHat);



        _inputs = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;  // GetAxisRaw should be sharper
        _attack = Input.GetAxisRaw("Attack");


        // GET BUTTON DOWN (with axis)
        if (Input.GetAxisRaw("Attack") != 0)
        {
            if (m_isAxisInUse == false)
            {
                // Call your event function here.
                m_isAxisInUse = true;

                // trigger attack animation
                animator.SetBool("isAttack", true);
                cameraFollowPlayer.Attack();

                // set has hat false next frame...
                _hasHat = false;
            }
        }
        if (Input.GetAxisRaw("Attack") == 0)
        {
            m_isAxisInUse = false;

            animator.SetBool("isAttack", false);
        }
    }

    void FixedUpdate() // APPLY CONTINUOUS PHYSICS
    {
        _body.velocity = _inputs * speed;

        // Set animator parameters
        if (_inputs == Vector2.zero)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }

        //playerMove = (_inputs * speed * Time.fixedDeltaTime);
        //_body.MovePosition(_body.position + playerMove);
    }
}
