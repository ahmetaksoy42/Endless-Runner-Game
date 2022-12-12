using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Distance : MonoBehaviour
{
    public GameObject disBorder;
    
    public int score=1;
    
    public bool addingDis = false;

    public float addDistanceTime =0.25f;

    public static Distance Instance;

    public GameObject gameOverPanel;

    public TextMeshProUGUI highscoreText;

    private void Awake()
    {
        Instance = this;
    }


    void Update()
    {
        if(addingDis == false)
        {
            addingDis = true;
            StartCoroutine(AddingDis());
        }
        if (score > PlayerPrefs.GetInt("HighDistance", score)) 
        {
            PlayerPrefs.SetInt("HighDistance", score);
        }
        highscoreText.text = "Highscore : " + PlayerPrefs.GetInt("HighDistance", score).ToString();

    }

    IEnumerator AddingDis()
    {
        score += 1;
        disBorder.GetComponent<Text>().text = "" + score;
        yield return new WaitForSeconds(addDistanceTime);
        addingDis = false;
    }
}
