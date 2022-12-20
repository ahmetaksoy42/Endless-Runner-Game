using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] roadTiles;

    Vector3 nextSpawnPoint;

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(roadTiles[tileIndex], nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = go.transform.GetChild(1).transform.position;
    }
    void Start()
    {
        SpawnTile(0);
        for(int i = 0; i < 20; i++)
        {
            SpawnTile(Random.Range(1,10));
        }
    }
}
