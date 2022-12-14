using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UntouchableSkill : MonoBehaviour
{
    public int x;

    private Player player;

    private Distance distance;

    private Rigidbody rb;

    void Start()
    {
        player = Player.Instance;

        distance = Distance.Instance;

        rb= player.player.GetComponent<Rigidbody>();

        gameObject.SetActive(false);

        x = Random.Range(0, 3);

        if (x == 2)
        {
            gameObject.SetActive(true);
        }

    }

    private void Update()
    {
        transform.Rotate(1, 1, 1, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Untouchable();
        }
        Invoke("BackToNormal", 8f);

        Invoke("BackToNormal2", 10f);
    }
        

    void Untouchable()
    {
        // player.player.GetComponent<Rigidbody>().isKinematic = true;

        rb.constraints = RigidbodyConstraints.FreezePositionY;

        player.player.GetComponent<CapsuleCollider>().isTrigger = true;

        player.player.GetComponent<TrailRenderer>().enabled = true;

        player.moveSpeed = player.moveSpeed * 2;

        distance.scoreAdd = 2;

    }

    void BackToNormal()
    {
        player.player.GetComponent<TrailRenderer>().enabled = false;

        player.moveSpeed = player.moveSpeed / 2;

        distance.scoreAdd = 1;
    }
    void BackToNormal2()
    {
        player.player.GetComponent<CapsuleCollider>().isTrigger = false;

        rb.constraints = RigidbodyConstraints.None;

        rb.constraints = RigidbodyConstraints.FreezeRotation;

    }
}
