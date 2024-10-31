using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Vector2 direction = Vector2.right;

    public Transform segmentPrefab;

    [SerializeField]
    private List<Transform> segments;

    private void Awake()
    {
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

    public void Grow()
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
            SnakeDied();
        }
    }

    private void SnakeDied()
    {
        Debug.Log("Snake died");
    }
}
