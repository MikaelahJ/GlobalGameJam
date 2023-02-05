using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI GUICounter;
    public GameObject completeGUI;
    public float gameOverCounter;
    public bool countDownStarted = false;
    public float timeLimitTotal = 10.0f;
    public int i_timeLimitTotal;
    public bool theFinalCountdoooooooown = false;

    private void Start()
    {
        StartCountdown();
    }
    void Update()
    {
        if(ResourceManager.Instance.getWaterSupply() <= 0 && !countDownStarted)
        {
            countDownStarted = true;            
            StartCountdown();
        }
        if (theFinalCountdoooooooown) {
            countdown();
            if (gameOverCounter < 0)
            {
                GAMEOVER();
            }
        }

    }

    public void StartCountdown()
    {
        theFinalCountdoooooooown = true;
        gameOverCounter = timeLimitTotal;
        completeGUI.SetActive(true);
    }
    public void countdown()
    {
        gameOverCounter -= Time.deltaTime * 1;
        i_timeLimitTotal = (int)gameOverCounter;
        GUICounter.text = "" + i_timeLimitTotal;
        if ((int)ResourceManager.Instance.getWaterSupply() > 0) foundWater();
    }
    public void foundWater()
    {
        theFinalCountdoooooooown = false;
        countDownStarted = false;
        completeGUI.SetActive(false);
    }
    public void GAMEOVER()
    {
        Debug.Log("time to end");
        SceneManager.LoadScene(3);
    }
    
}
