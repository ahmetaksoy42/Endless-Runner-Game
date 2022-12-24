using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float speed;
    public float distance = 4.0f;
    private Vector3 startPos;

    void Start()
    {
        if (gameObject.name == "car3")
        {
            distance = 2f;
        }
        speed = Random.Range(0.7f, 1.3f);

        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + new Vector3(Mathf.Sin(Time.time * speed), 0, 0) * distance;
    }
}
