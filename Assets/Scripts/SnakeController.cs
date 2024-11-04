using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Vector2 direction = Vector2.right;

    private bool hasShield;
    public Transform segmentPrefab;

    [SerializeField]
    private List<Transform> segments;

    [SerializeField]
    private int segmentLength;//no of units by which length increases or decreases

    public ScoreController scoreController;
    public PowerupController powerupController;
    public GameObject gameoverController;
    private void Awake()
    {
        hasShield = false;
        segments = new List<Transform>();
        segments.Add(transform);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Pressed w");
            if (direction!=Vector2.down)
                direction = Vector2.up;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Pressed s");
            if (direction != Vector2.up)
                direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Pressed a");
            if (direction != Vector2.right)
                direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Pressed d");
            if (direction != Vector2.left)
                direction = Vector2.right;
        }
    }
    private void FixedUpdate()
    {
        //changing position transform of each segment for movement
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        //changing transform of snake head for movement
        transform.position = new Vector3(Mathf.Round(transform.position.x)+direction.x, Mathf.Round(transform.position.y)+direction.y,0.0f);
    }

    private void Grow()
    {
        Debug.Log("snake grew in length");

        //adds segmentLength number of new segments 
        for (int i = 1; i <= segmentLength; i++)
        {
            Transform newSegment = Instantiate(segmentPrefab);
            newSegment.position = segments[segments.Count - 1].position;

            segments.Add(newSegment);
        }
    }

    private void Reduce()
    {
        Debug.Log("Snake decreaed in length");

        //removes segmentLength number of new segments 
        for (int i = 1; i <= segmentLength; i++)
        {
            if (segments.Count > 1)
            {
                Destroy(segments[segments.Count - 1].gameObject);
                segments.RemoveAt(segments.Count - 1);
            }
        }
    }

    private void Warp(GameObject wall)
    {
        if (direction == Vector2.down)
            transform.position = new Vector3(transform.position.x, -wall.transform.position.y - 1, 0.0f);
        else if (direction == Vector2.up)
            transform.position = new Vector3(transform.position.x, -wall.transform.position.y + 1, 0.0f);
        else if (direction == Vector2.right)
            transform.position = new Vector3(-wall.transform.position.x + 1, transform.position.y, 0.0f);
        else if (direction == Vector2.left)
            transform.position = new Vector3(-wall.transform.position.x - 1, transform.position.y, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="wall")
        {
            Warp(collision.gameObject);
        }

        if(collision.tag=="snakebody")
        {
            if(!hasShield)
                SnakeDied();
        }

        if(collision.tag=="massgainer")
        {
            Grow();
            scoreController.UpdateScore(2);//putting 2 as score value
        }

        if(collision.tag == "massburner")
        {
            Reduce();
            scoreController.UpdateScore(-2);//putting -2 as score value
        }

        if(collision.tag=="powerup")
        {
            Debug.Log("Picked up powerup");

            PowerupType ptype = collision.gameObject.GetComponent<PowerupInfo>().GetPowerupType();

            if (ptype == PowerupType.Multiplier)
            {
                scoreController.MultiplierUpdate(2);
                powerupController.RefreshUI(ptype, true);
                StartCoroutine(MultiplierTimer(ptype));
            }
            else if (ptype == PowerupType.Shield)
            {
                hasShield = true;
                powerupController.RefreshUI(ptype,true);
                StartCoroutine(ShieldTimer(ptype));
            }
                Destroy(collision.gameObject);
        }
    }

    private void SnakeDied()
    {
        Debug.Log("Snake died");
        enabled = false;
        gameoverController.SetActive(true);
    }

    IEnumerator ShieldTimer(PowerupType p)
    {
        yield return new WaitForSeconds(5);
        hasShield = false;
        powerupController.RefreshUI(p, false);
    }

    IEnumerator MultiplierTimer(PowerupType p)
    {
        yield return new WaitForSeconds(5);
        scoreController.MultiplierUpdate(1);
        powerupController.RefreshUI(p, false);
    }
}
