using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Distance : MonoBehaviour
{
    public int scoreAdd = 1;

    public GameObject disBorder;
    
    public int score=1;
    
    public bool addingDis = false;

    public float addDistanceTime =0.25f;

    public static Distance Instance;

    public GameObject gameOverPanel;

    public TextMeshProUGUI highscoreText;

    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        Instance = this;
    }


    void Update()
    {
        if (!Player.Instance.isGameStarted)
        {
            return;
        }
        if(addingDis == false)
        {
            addingDis = true;
            StartCoroutine(AddingDis());
        }

        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        ScoreDisplay();

    }

    public IEnumerator AddingDis()
    {
        score += scoreAdd;
        disBorder.GetComponent<TextMeshProUGUI>().text = "" + score;
        yield return new WaitForSeconds(addDistanceTime);
        addingDis = false;
    }

    void ScoreDisplay()
    {
        scoreText.text = "Your Score :" + score;

        highscoreText.text = "High Score : " + PlayerPrefs.GetInt("HighScore").ToString();
    }
}
