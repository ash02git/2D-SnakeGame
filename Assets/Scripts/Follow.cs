using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public Transform direction;
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, direction.position, speed * Time.deltaTime);
    }

    public void ChangeAngle(Quaternion a)
    {
        Debug.Log("Passed rotation is " + a);
        transform.localRotation = a;
    }
}
