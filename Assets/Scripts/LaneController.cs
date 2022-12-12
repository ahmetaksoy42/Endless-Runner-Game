using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        Vector3 targetPos = player.position;

        targetPos.x = 0;

        transform.position = targetPos;
    }
}
