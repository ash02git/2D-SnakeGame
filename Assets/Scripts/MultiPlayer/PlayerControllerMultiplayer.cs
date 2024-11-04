using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMultiplayer : MonoBehaviour
{
    private Vector2 direction = Vector2.right;

    public Transform segmentPrefab;

    [SerializeField]
    private List<Transform> segments;
    [SerializeField]
    private int segmentLength;
    public string playerNumber;

    public PlayerControllerMultiplayer opponent;
    public ScoreControllerMultiplayer scm;
    public GameObject gcm;
    private void Awake()
    {
        segments = new List<Transform>();
        segments.Add(transform);
    }

    private void Update()
    {
        switch (playerNumber)
        {
            case "Player1":Player1Controls();
                break;
            case "Player2":Player2Controls();
                break;
        }

    }

    private void Player1Controls()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Pressed w");
            if (direction != Vector2.down)
                direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
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
    private void Player2Controls()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Pressed w");
            if (direction != Vector2.down)
                direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Pressed s");
            if (direction != Vector2.up)
                direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Pressed a");
            if (direction != Vector2.right)
                direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
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
        transform.position = new Vector3(Mathf.Round(transform.position.x) + direction.x, Mathf.Round(transform.position.y) + direction.y, 0.0f);
    }

    private void Grow()
    {
        Debug.Log("snake grew in length");

        for (int i = 1; i <= segmentLength; i++)
        {
            Transform newSegment = Instantiate(segmentPrefab);
            newSegment.position = segments[segments.Count - 1].position;

            segments.Add(newSegment);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(opponent.playerNumber) )
        {
            Debug.Log(playerNumber + "has won");
            enabled = false;
            opponent.enabled = false;
            
            StartCoroutine(DelayedGameOver());
        }
        if(collision.gameObject.CompareTag(playerNumber))
        {
            Debug.Log(opponent.playerNumber + "has won");
            enabled = false;
            opponent.enabled = false;
            StartCoroutine(DelayedGameOver());
        }
        if (collision.tag == "wall")
        {
            if (direction == Vector2.down)
                transform.position = new Vector3(transform.position.x, -collision.transform.position.y - 1, 0.0f);
            else if (direction == Vector2.up)
                transform.position = new Vector3(transform.position.x, -collision.transform.position.y + 1, 0.0f);
            else if (direction == Vector2.right)
                transform.position = new Vector3(-collision.transform.position.x + 1, transform.position.y, 0.0f);
            else if (direction == Vector2.left)
                transform.position = new Vector3(-collision.transform.position.x - 1, transform.position.y, 0.0f);
        }
        if (collision.tag == "massgainer")
        {
            Grow();
            scm.UpdateScore(2);//putting 2 as score value
        }
    }

    private IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(2.0f);
        gcm.SetActive(true);
        gcm.GetComponent<GameoverControllerMultiplayer>().ShowWinner(playerNumber);
    }
}
