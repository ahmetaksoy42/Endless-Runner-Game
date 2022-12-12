﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTile : MonoBehaviour
{
    RoadSpawner roadSpawner;

    void Start()
    {
        roadSpawner = GameObject.FindObjectOfType<RoadSpawner>();
    }
    private void OnTriggerExit(Collider other)
    {
        roadSpawner.SpawnTile(Random.Range(1,6));

        Destroy(gameObject, 3f);
    }

}