using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    private float cooldownTime = 3f;
    public BoxCollider2D GridArea;

    private Coroutine current;

    private void Start()
    {
        current = StartCoroutine(LoopedSpawning());
    }

    IEnumerator LoopedSpawning()
    {
        RandomizePosition();

        yield return new WaitForSeconds(cooldownTime);
        current = StartCoroutine(LoopedSpawning());
    }

    private void RandomizePosition()
    {
        Bounds bounds = GridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //collision.GetComponent<SnakeController>() != null
        {
            StopCoroutine(current);
            current = StartCoroutine(LoopedSpawning());
        }
    }
}

