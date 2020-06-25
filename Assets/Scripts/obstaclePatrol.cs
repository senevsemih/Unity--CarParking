using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstaclePatrol : MonoBehaviour
{
    float speed = 10.0f;

    void Update()
    {
       transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        transform.Rotate(Vector3.right, 180);
    }
}
