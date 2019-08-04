using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyHealth : MonoBehaviour
{
    private int maxHealth = 1;
    private int health;

    private bool dying = false;

    private const string SHADER_COLOR_NAME = "_Color";
    private Material material;

    private GameObject explosion;
    private Animator explosionAnimator;

    private float fracJourney = 0f;

    private float color;

    private float startTime;

    void Start()
    {
        explosion = transform.GetChild(0).gameObject;
        explosionAnimator = explosion.GetComponentInChildren<Animator>();

        // makes a new instance of the material for runtime changes
        material = GetComponent<SpriteRenderer>().material;
        SetColor(new Color(1, 1, 1, 0));

        health = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "hat")
        {
            health -= 1;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "hat")
        {
            health -= 1;
        }
    }

    void Update()
    {
        if (health <= 0 && dying == false)
        {
            dying = true;

            // DIE
            // trigger explosion animation (and audio)
            this.GetComponentInParent<AIPath>().canMove = false;  // halt movement

            SetColor(Color.white);

            explosionAnimator.SetBool("shouldExplode", true);
            this.transform.GetComponent<Animator>().SetBool("shouldDie", true);

            startTime = Time.time;

            StartCoroutine(slowDeath(0.75f));
        }
        else if (health <= 0)
        {
            // TODO: slow fade to transparent
            float distCovered = (Time.time - startTime);
            float journeyLength = 0.75f;    // num seconds

            fracJourney = distCovered / journeyLength;
            fracJourney = Mathf.Sqrt(Mathf.Sqrt(fracJourney));


            color = Mathf.Lerp(1, 0, fracJourney);
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, color);
        }
    }

    private void SetColor(Color color)
    {
        material.SetColor(SHADER_COLOR_NAME, color);
    }

    private IEnumerator slowDeath(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // DO STUFF
        transform.parent.gameObject.SetActive(false);
    }
}
