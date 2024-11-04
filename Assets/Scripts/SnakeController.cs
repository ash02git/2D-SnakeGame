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
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x)+direction.x, Mathf.Round(transform.position.y)+direction.y,0.0f);
    }

    private void Grow()
    {
        Debug.Log("snake grew in length");
        Transform newSegment = Instantiate(segmentPrefab);
        newSegment.position = segments[segments.Count-1].position;

        segments.Add(newSegment);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="wall")
        {
            if (direction == Vector2.down)
                transform.position = new Vector3(transform.position.x, - collision.transform.position.y - 1,0.0f);
            else if(direction == Vector2.up)
                transform.position = new Vector3(transform.position.x, -collision.transform.position.y + 1, 0.0f);
            else if(direction == Vector2.right)
                transform.position = new Vector3(-collision.transform.position.x + 1, transform.position.y, 0.0f);
            else if (direction == Vector2.left)
                transform.position = new Vector3(-collision.transform.position.x - 1, transform.position.y, 0.0f);
        }

        if(collision.tag=="snakebody")
        {
            if(!hasShield)
                SnakeDied();
        }

        if(collision.tag=="food")
        {
            Grow();
            scoreController.UpdateScore(2);//putting 2 as score value
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
