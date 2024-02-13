using Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameState gameState;
    public GameObject player;
    public GameObject laser;
    public GameObject bomb;
    public int maxLaser = 5;
    public int maxBomb = 2;
    public List<GameObject> laserList = new List<GameObject>();
    public List<GameObject> bombList = new List<GameObject>();
    public int hp;
    public int atk;
    public int atkCD;
    public int currHp;
    public GameObject gameManager;
    public bool canAttack;

    private int currentLaser = 0;
    private int currentBomb = 0;
    // Start is called before the first frame update
    void Awake()
    {
        canAttack = true;
        if (GameObject.FindGameObjectWithTag("GameController") != null)
        {
            for (int i = 0; i < maxLaser; i++)
            {
                var newLaser = Instantiate(laser);
                newLaser.SetActive(false);
                laserList.Add(newLaser);
            }
            for (int i = 0; i < maxBomb; i++)
            {
                var newBomb = Instantiate(bomb);
                newBomb.SetActive(false);
                bombList.Add(newBomb);
            }
            hp = 100;
            atk = 20;
            currHp = hp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        gameState = gameManager.gameObject.GetComponent<GameManager>().GetGameState();
        // Make monster focus player
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Vector3 look = transform.InverseTransformPoint(player.transform.position);
            float angle = Mathf.Atan2(look.y,look.x) * Mathf.Rad2Deg - 270;
            this.transform.Rotate(0,0,angle);
            //this.transform.LookAt(new Vector3 (player.transform.position.x,player.transform.position.y,player.transform.position.z));
        }
    }

    private void FixedUpdate()
    {
        if (canAttack && gameState == GameState.Gameplay)
        {
            // Debug.Log("attack");
            Attack();
        }
    }

    void Attack()
    {
        StartCoroutine(AttackDelayCoroutine());
        if (currentLaser == maxLaser)
        {
            currentLaser = 0;
        }

        if (currentBomb == maxBomb)
        {
            currentBomb = 0;
        }

        Vector3 direction = player.transform.position - transform.position;
        int randAttack = Random.Range(1, 10);
        if (randAttack >= 3)
        {
            laserList[currentLaser].SetActive(true);
            laserList[currentLaser].transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
            float randx = Random.Range(direction.x - 1f, direction.x + 1f);
            float randy = Random.Range(direction.y - 1f, direction.y + 1f);
            float randz = Random.Range(direction.z - 1f, direction.z + 1f);
            laserList[currentLaser].GetComponent<Rigidbody>().velocity = new Vector3(direction.x, direction.y - 1f, direction.z).normalized * 10;
            float rotX = Mathf.Atan2(direction.y - 1f, Mathf.Sqrt(direction.x * direction.x + direction.z * direction.z)) * Mathf.Rad2Deg;
            float rotY = Mathf.Atan2(-direction.x, -direction.z) * Mathf.Rad2Deg;
            float rotZ = 0f;
            laserList[currentLaser].transform.rotation = Quaternion.Euler(rotX - 90, rotY, rotZ);
            currentLaser++;
        }
        else
        {
            // Debug.Log("bomb");
            bombList[currentBomb].SetActive(true);
            bombList[currentBomb].transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
            // bombList[currentBomb].GetComponent<Rigidbody>().velocity = new Vector3(direction.x, direction.y, direction.z).normalized * 10;
            currentBomb++;
        }
        

    }

    private IEnumerator AttackDelayCoroutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(2f); // Adjust the delay time as needed
        canAttack = true;
    }

    public void DamageMonster(bool isNormal)
    {   
        if (isNormal)
        {
            currHp -= player.GetComponent<Player>().atk;
        }
        else
        {
            currHp -= player.GetComponent<Player>().atk * 2;
        }

        if (currHp <= 0 )
        {
            Debug.Log("Monster Dead!");
            gameManager = GameObject.FindGameObjectWithTag("GameController");
            player.GetComponent<Player>().IncreaseScore();
            player.GetComponent<Player>().IncreaseMoney(50);
            player.GetComponent<Player>().IncreaseLevel();
            // gameManager.GetComponent<GameManager>().GameOver(true);
            gameManager.GetComponent<GameManager>().Shopping();
        }
    }

    public int GetAtk()
    {
        return atk;
    }

    public int GetCurrentHp()
    {
        return currHp;
    }

    public void SetMonsterStats()
    {
        currHp = 100 * player.GetComponent<Player>().lv;
        hp = currHp;
        atk = 20 * player.GetComponent<Player>().lv;
    }

    public void RespawnMonster()
    {
        int playerLV = player.GetComponent<Player>().lv;
        hp = 100 * playerLV;
        atk = 20 * playerLV;
        currHp = hp;
    }

    public GameObject GetMonster()
    {
        return this.gameObject;
    }
}
