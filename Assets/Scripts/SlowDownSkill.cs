using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownSkill : MonoBehaviour
{
    public int x;

    private Player player;

    MeshRenderer meshRenderer;


    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();

        player = Player.Instance;

        gameObject.SetActive(false);

        x = Random.Range(0, 3);

        if (x == 2)
        {
            gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        transform.Rotate(0.5f, 0.5f, 0.5f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SlowDown();

            var playerTransform = new Vector3(other.transform.position.x, other.transform.position.y + 2, other.transform.position.z);
            transform.parent = other.transform;
            transform.localScale = transform.localScale / 2;
            transform.position = playerTransform;
        }
        Invoke("NormalSpeed", 8f);
    }

    void SlowDown()
    {
        var slowspeed = player.moveSpeed / 2;
        var slowHorizontal = player.horizontalSpeed / 2;
        player.moveSpeed = slowspeed;
        player.horizontalSpeed = slowHorizontal;
    }

    void NormalSpeed()
    {
        /*
        player.moveSpeed = player.moveSpeed;

        player.horizontalSpeed = player.horizontalSpeed;
        */
    }
}
