using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 1;
    private int health;

    private bool dying = false;

    private const string SHADER_COLOR_NAME = "_Color";
    private Material material;

    private float fracJourney = 0f;

    private float color;

    private float startTime;

    public GameObject player;
    public PlayerMovement playerMovement;
    public CameraFollowPlayer cameraFollowPlayer;
    public RotateToCursor rotateToCursor;

    void Start()
    {
        // makes a new instance of the material for runtime changes
        material = player.GetComponent<SpriteRenderer>().material;
        SetColor(new Color(1, 1, 1, 0));

        health = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            health -= 1;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
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
            // halt movement

            SetColor(Color.white);

            startTime = Time.time;

            playerMovement.isAlive = false;

            player.gameObject.GetComponent<Rigidbody2D>().simulated = false;

            rotateToCursor.isAlive = false;
            cameraFollowPlayer.isAlive = false;

            StartCoroutine(slowDeath(1.5f));
        }
        else if (health <= 0)
        {
            // TODO: slow fade to transparent
            float distCovered = (Time.time - startTime);
            float journeyLength = 1.5f;    // num seconds

            fracJourney = distCovered / journeyLength;
            fracJourney = Mathf.Sqrt(Mathf.Sqrt(fracJourney));


            color = Mathf.Lerp(1, 0, fracJourney);
            player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, color);
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
