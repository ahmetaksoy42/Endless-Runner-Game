using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UntouchableSkill : MonoBehaviour
{
    public int x;

    private Player player;

    private Distance distance;

    private Rigidbody rb;

    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();

        player = Player.Instance;

        distance = Distance.Instance;

        rb= player.GetComponent<Rigidbody>();

        gameObject.SetActive(false);

        x = Random.Range(0, 2);

        if (x == 1)
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
            player.isUntouchable = true;

            meshRenderer.enabled = false;

            Untouchable();
        }
        Invoke("BackToNormal", 8f);

        //Invoke("BackToNormal2", 10f);
    }
        

    void Untouchable()
    {
        // player.player.GetComponent<Rigidbody>().isKinematic = true;

        //rb.constraints = RigidbodyConstraints.FreezePositionY;

       // player.player.GetComponent<CapsuleCollider>().isTrigger = true;

       // player.player.GetComponent<TrailRenderer>().enabled = true;

        player.moveSpeed = player.moveSpeed * 1.2f;

        distance.scoreAdd = 2;
    }

    void BackToNormal()
    {
        player.isUntouchable = false;

        player.moveSpeed = player.moveSpeed / 1.2f;

        distance.scoreAdd = 1;
    }
    void BackToNormal2()
    {
       // player.player.GetComponent<CapsuleCollider>().isTrigger = false;

       // rb.constraints = RigidbodyConstraints.None;

      //  rb.constraints = RigidbodyConstraints.FreezeRotation;

    }
   
}
