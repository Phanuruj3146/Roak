using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Core;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeTxt;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI spdText;
    public TextMeshProUGUI bossHpText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI lvText;
    public float timeRemaining = 30;
    public GameState gameState;
    public GameObject player;
    public GameObject monster;
    public GameObject shopManager;
    public Button respawnBtn;
    public GameObject monObj;

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
            player = GameObject.FindGameObjectWithTag("Player");
            monster = GameObject.FindGameObjectWithTag("Monster");
            monster = monster.GetComponent<Monster>().GetMonster();
            if (timeRemaining > 1 && player.GetComponent<Player>().GetHp() >= 0)
            {
                // Time Countdown
                timeRemaining -= Time.deltaTime;
                float min = Mathf.Abs(Mathf.FloorToInt(timeRemaining / 60));
                float sec = Mathf.Abs(Mathf.FloorToInt(timeRemaining % 60));
                outputText = "Time left: " + string.Format("{0:00}:{1:00}", min, sec);
                timeTxt.text = outputText;

                // Player LV
                int playerLv = player.GetComponent<Player>().lv;
                lvText.text = "Level:" + playerLv;

                // Player HP
                int playerHp = player.GetComponent<Player>().GetHp();
                int playerCurrentHp = player.GetComponent<Player>().GetCurrentHp();
                string playerHpStat = "HP:" + playerCurrentHp + "/" + playerHp;
                hpText.text = playerHpStat;

                // Player ATK
                int playerAtk = player.GetComponent<Player>().GetAtk();
                atkText.text = "ATK:" + playerAtk;

                // Player SPD
                int playerSpd = player.GetComponent<Player>().GetSpd();
                spdText.text = "SPD:" + playerSpd;

                // Boss HP
                int bossHp = monster.GetComponent<Monster>().hp;
                int bossCurrentHp = monster.GetComponent<Monster>().GetCurrentHp();
                string bossHpStat = "HP:" + bossCurrentHp + "/" + bossHp;
                bossHpText.text = bossHpStat;
            } else
            {
                // Time run out
                gameState = GameState.Postgame;
            }
        } else if (gameState == GameState.Shopping)
        {
            shopManager.SetActive(true);
            int score = player.GetComponent<Player>().score;
            scoreTxt.text = "Score:" + score.ToString();

            // Money
            int money = player.GetComponent<Player>().money;
            moneyText.text = "Money:" + money.ToString();
            if (monster.GetComponent<Monster>().GetCurrentHp() <= 0)
            {
                int bossHp = monster.GetComponent<Monster>().hp;
                int bossCurrentHp = monster.GetComponent<Monster>().GetCurrentHp();
                string bossHpStat = "HP:" + bossCurrentHp + "/" + bossHp;
                bossHpText.text = bossHpStat;
            }

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

    public void PlayerHit()
    {
        player.GetComponent<Player>().DamagePlayer(monster.GetComponent<Monster>().GetAtk());
    }

    public void RespawnMonster()
    {
        monster.GetComponent<Monster>().SetMonsterStats();
        monster.SetActive(true);
        timeRemaining = 30;
        gameState = GameState.Gameplay;
    }
}
