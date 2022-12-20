using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSetActive : MonoBehaviour
{
    public int a;

    private void Start()
    {
        gameObject.SetActive(false);

        a = Random.Range(0, 4);

        if (a == 2)
        {
            gameObject.SetActive(true);
        }
    }
}
