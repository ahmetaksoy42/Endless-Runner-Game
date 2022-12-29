using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject startingText;

    public static Slider timerSlider;

    public bool stopTimer;

    private void Start()
    {
        timerSlider = GameObject.Find("TimerSlider").GetComponent<Slider>();

        timerSlider.gameObject.SetActive(false);

        stopTimer = false;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Level");
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Player.Instance.isGameStarted = true;
        }
        if (Player.Instance.isGameStarted)
        {
            Destroy(startingText);
        }
    }

    public static void TimeSlider(int maxValue,int minValue)
    {
        timerSlider.gameObject.SetActive(true);

        timerSlider.maxValue = maxValue;

        timerSlider.minValue = minValue;

        timerSlider.wholeNumbers = false;

        timerSlider.value = maxValue;

        float time = timerSlider.value;

        if (timerSlider.value > timerSlider.minValue)
        {
            time -= Time.deltaTime;

            timerSlider.value = time;

            //timerSlider.gameObject.SetActive(false);
        }
        else if(timerSlider.value <= timerSlider.minValue)
        {
            timerSlider.gameObject.SetActive(false);
        }
    }
}
