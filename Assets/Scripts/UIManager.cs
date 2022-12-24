using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject startingText;

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
}
