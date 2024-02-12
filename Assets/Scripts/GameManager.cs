using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Core;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeTxt;
    public TextMeshProUGUI scoreTxt;
    public float timeRemaining = 30;
    public GameState gameState;


    private string outputText;
    // Start is called before the first frame update
    void Start()
    {
        float min = Mathf.FloorToInt(timeRemaining / 60);
        float sec = Mathf.FloorToInt(timeRemaining % 60);
        outputText = "Time left: " + string.Format("{0:00}:{1:00}", min, sec);
        timeTxt.text = outputText;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.Gameplay)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                float min = Mathf.FloorToInt(timeRemaining / 60);
                float sec = Mathf.FloorToInt(timeRemaining % 60);
                outputText = "Time left: " + string.Format("{0:00}:{1:00}", min, sec);
                //timeTxt.text = "Score:" + score.ToString();
                timeTxt.text = outputText;
            }
            } else{
                gameState = GameState.Postgame;
        }
    }

    public void StartGame()
    {
        gameState = GameState.Gameplay;
    }

    public void Shopping()
    {
        gameState = GameState.Shopping;
    }

    public GameState GetGameState()
    {
        return gameState;
    }
}
