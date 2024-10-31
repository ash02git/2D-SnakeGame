using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public GameObject[] body;

    public Transform direction;
    
    void Update()
    {
       transform.position = Vector2.MoveTowards(transform.position, direction.position, speed*Time.deltaTime);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        LeftRight(horizontal);
        UpDown(vertical);
    }

    private void LeftRight(float horizontal)
    {
        if(horizontal > 0)
        {
            Debug.Log("right movement");

            if (transform.localRotation != Quaternion.Euler(0, 0, 180))
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(BodyMoves(Quaternion.Euler(0, 0, 0)));
            }
        }
        else if(horizontal < 0)
        {
            Debug.Log("left movement");

            if (transform.localRotation != Quaternion.Euler(0, 0, 0))
            {
                transform.localRotation = Quaternion.Euler(0, 0, 180);
                StartCoroutine(BodyMoves(Quaternion.Euler(0, 0, 180)));
            }
        }

        
    }

    private void UpDown(float vertical)
    {
        if (vertical > 0)
        {
            Debug.Log("Up movement");

            if (transform.localRotation != Quaternion.Euler(0, 0, -90))
            {
                transform.localRotation = Quaternion.Euler(0, 0, 90);
                StartCoroutine(BodyMoves(Quaternion.Euler(0, 0, 90)));
            }
        }
        else if (vertical < 0)
        {
            Debug.Log("Down movement");

            if (transform.localRotation != Quaternion.Euler(0, 0, 90))
            {
                transform.localRotation = Quaternion.Euler(0, 0, -90);
                StartCoroutine(BodyMoves(Quaternion.Euler(0, 0, -90)));
            }
        }   
    }

    IEnumerator BodyMoves(Quaternion a)
    {
        for (int i = 0; i < body.Length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            body[i].GetComponent<Follow>().ChangeAngle(a);
        }
    }
}
