using System.Collections;
using System.Collections.Generic;
using Core;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Button upHp;
    public Button upAtk;
    public Button upSpd;
    public GameObject player;
    public TextMeshProUGUI money;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI spd;
    public int playerMoney;
    public GameObject gameManager;
    public GameState gameState;
    public Button healBtn;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameState = gameManager.GetComponent<GameManager>().GetGameState();
        if (gameState == GameState.Shopping )
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerMoney = player.GetComponent<Player>().money;
                string playerHpText = "HP:" + player.GetComponent<Player>().hp;
                string playerAtkText = "ATK:" + player.GetComponent<Player>().atk;
                string playerSpdText = "SPD:" + player.GetComponent<Player>().spd;
                string playerMoneyText = "Money:" + player.GetComponent<Player>().money;

                hp.text = playerHpText;
                atk.text = playerAtkText;
                spd.text = playerSpdText;
                money.text = playerMoneyText;
            }
        } else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void BuyUpgradeHp()
    {
        if (playerMoney >= 10)
        {
            Debug.Log("Upgrade HP");
            player.GetComponent<Player>().UpgradeHP();
            player.GetComponent<Player>().DecreaseMoney(10);
        }
    }
    public void BuyUpgradeAtk()
    {
        if (playerMoney >= 10)
        {
            Debug.Log("Upgrade ATK");
            player.GetComponent<Player>().UpgradeATK();
            player.GetComponent<Player>().DecreaseMoney(10);
        }
    }
    public void BuyUpgradeSpd()
    {
        if (playerMoney >= 10)
        {
            Debug.Log("Upgrade SPD");
            player.GetComponent<Player>().UpgradeSPD();
            player.GetComponent<Player>().DecreaseMoney(10);
        }
    }

    public void BuyHeal()
    {
        if (playerMoney >= 20)
        {
            player.GetComponent<Player>().Heal();
            player.GetComponent<Player>().Heal();
            player.GetComponent<Player>().DecreaseMoney(20);
        }
    }

    
}
