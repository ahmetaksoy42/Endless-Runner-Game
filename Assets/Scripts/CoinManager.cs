using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int coinCount;

    public int rotateSpeed = 1;

    public GameObject coinCountDisplay;

    public static CoinManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        coinCount = 0;
    }

    void Update()
    {
        //coinCountDisplay.GetComponent<Text>().text = "" + coinCount;
        coinCountDisplay.GetComponent<TextMeshProUGUI>().text = ""+coinCount;

    }
}
