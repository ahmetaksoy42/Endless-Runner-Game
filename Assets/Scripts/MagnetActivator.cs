﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetActivator : MonoBehaviour
{
    public int x;

    private void Start()
    {
        gameObject.SetActive(false);
        x = Random.Range(0,20);

        if (x == 2)
        {
            gameObject.SetActive(true);
        }
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
