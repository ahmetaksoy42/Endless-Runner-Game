using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoin : MonoBehaviour
{
    public int rotateSpeed = 1;

    void Update()
    {
        transform.Rotate(0, rotateSpeed*Time.deltaTime, 0,Space.World);
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);

        CanvasManager.coinCount++;
    }
    */
}
