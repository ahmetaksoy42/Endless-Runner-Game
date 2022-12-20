using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollower : MonoBehaviour
{
    public Transform player;

    Vector3 offset;

    private Player playerScript;

    Vector3 cameraVelocity = Vector3.zero;

    private void Start()
    {
        playerScript = Player.Instance;

        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, playerScript.lanes[playerScript.laneIndex].transform.position, 20 * Time.fixedDeltaTime);

        Vector3 targetPos = player.position + offset;

       // transform.position = Vector3.Lerp(transform.position, playerScript.lanes[playerScript.laneIndex].transform.position, playerScript.horizontalSpeed * Time.fixedDeltaTime);
        /*
        if (playerScript.laneIndex == 0)
        {
            //targetPos.x = player.position.x-0.1f;
        }
        else if (playerScript.laneIndex == 1)
        {
            targetPos.x = player.position.x;
        }
        else
        {
            targetPos.x = player.position.x + 0.1f;
        }
        */
       // targetPos.x = 0;
       // targetPos.y = 5.412974f;
        targetPos.y = 8.412974f;

        //transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(playerScript.lanes[playerScript.laneIndex].transform.position.x,7,targetPos.z), playerScript.horizontalSpeed * Time.fixedDeltaTime);

        transform.position = targetPos;
    }
}
