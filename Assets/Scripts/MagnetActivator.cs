using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetActivator : MonoBehaviour
{
    public int x;
    public float speed = 3.0f;
    public float distance = 0.2f;
    private Vector3 startPos;

    private void Start()
    {
        gameObject.SetActive(false);

        x = Random.Range(0,20);

        if (x == 2)
        {
            gameObject.SetActive(true);
        }

        startPos = transform.position;
    }
    private void Update()
    {
       // transform.Rotate(0, 10*Time.deltaTime, 0, Space.World);

        transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time * speed), 0) * distance;
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Magnet.isMagnetActive = true;
            StartCoroutine(ActivateMagnet());
            Magnet.isMagnetActive = false;
        } 
    }

    IEnumerator ActivateMagnet()
    {
        yield return new WaitForSeconds(8f);
    }*/
}
