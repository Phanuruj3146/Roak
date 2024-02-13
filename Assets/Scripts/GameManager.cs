using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Core;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

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
    public GameObject buffBox;
    public List<GameObject> boxList = new List<GameObject>();
    public int maxBox = 3;
    public Vector3 monsterPos;
    public GameObject uiController;
    public GameObject gameover;
    public TextMeshProUGUI finalScore;
    public Button startAgain;
    public ARSession arSession;
    public GameObject crosshair;

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

            if (timeRemaining > 1 && player.GetComponent<Player>().GetCurrentHp() > 0)
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
            } else if (player.GetComponent<Player>().GetCurrentHp() < 0)
            {
                // Time run out
                // Player HP

                int playerHp = player.GetComponent<Player>().GetHp();
                int playerCurrentHp = player.GetComponent<Player>().GetCurrentHp();
                string playerHpStat = "HP:" + playerCurrentHp + "/" + playerHp;
                hpText.text = playerHpStat;
                uiController.SetActive(false);
                gameState = GameState.Postgame;

                gameover.SetActive(true);
                finalScore.text = "Score:" + player.GetComponent<Player>().score;

                Debug.Log("GAME OVER");
            }
        } else if (gameState == GameState.Shopping)
        {
            uiController.SetActive(false);
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
        uiController.SetActive(true);
    }

    public void GetMonsterVector3(Vector3 val)
    {
        monsterPos = val;
        float posx = monsterPos.x;
        float posy = monsterPos.y;
        float posz = monsterPos.z;
        for (int i = 0; i < maxBox; i++)
        {
            GameObject box = Instantiate(buffBox, new Vector3(Random.Range(posx-2,posx+4), Random.Range(posy, posy + 4), Random.Range(posz - 2, posz + 2)), Quaternion.identity);
            //GameObject box = Instantiate(buffBox, new Vector3(Random.Range(posx - 2, posx + 4), posy, Random.Range(posz - 2, posz + 2)), Quaternion.identity);
            boxList.Add(box);
        }
    }

    public void ResetTheGame()
    {
        uiController.SetActive(false);
        Destroy(monster);
        Destroy(player);
        for (int i = 0; i < maxBox; i++)
        {
            Destroy(boxList[i]);
        }

        crosshair.GetComponent<Crosshair>().Rescan();
        gameState = GameState.PreGame;
    }
}
