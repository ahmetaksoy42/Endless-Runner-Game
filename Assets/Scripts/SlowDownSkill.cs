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

        x = Random.Range(0, 20);

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

            meshRenderer.enabled = false;
        }
        Invoke("NormalSpeed", 8f);
    }

    void SlowDown()
    {
        player.moveSpeed = player.moveSpeed / 2;

        player.horizontalSpeed = player.horizontalSpeed / 2;
    }

    void NormalSpeed()
    {
        player.moveSpeed = player.moveSpeed * 2;

        player.horizontalSpeed = player.horizontalSpeed * 2;
    }
}
