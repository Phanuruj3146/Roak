using Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameState gameState;
    public GameObject player;
    public int maxLaser = 5;
    public GameObject laser;
    public List<GameObject> laserList = new List<GameObject>();
    public int hp;
    public int atk;
    public int atkCD;
    public float currHp;
    public GameObject gameManager;
    public bool canAttack;

    private int currentLaser = 0;
    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        if (GameObject.FindGameObjectWithTag("GameController") != null)
        {
            for (int i = 0; i < maxLaser; i++)
            {
                var newLaser = Instantiate(laser);
                newLaser.GetComponent<Renderer>().enabled = false;
                laserList.Add(newLaser);
            }
            int playerLV = player.gameObject.GetComponent<Player>().lv;
            hp = 100 * playerLV;
            atk = 20 * playerLV;
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
        Debug.Log(canAttack);
        Debug.Log(gameState);
        if (canAttack && gameState == GameState.Gameplay)
        {
            Debug.Log("attack");
            Attack();
        }
    }

    void Attack()
    {
        StartCoroutine(AttackDelayCoroutine());
        if (currentLaser == 5)
        {
            currentLaser = 0;
        }
        Vector3 direction = player.transform.position - transform.position;
        // Vector3 direction = player.transform.position;
        
        laserList[currentLaser].transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);
        // laserList[currentLaser].transform.rotation = player.transform.rotation;
        // laserList[currentLaser].transform.rotation = Quaternion.LookRotation(direction.normalized + new Vector3(1, 1, 1));
        laserList[currentLaser].GetComponent<Renderer>().enabled = true;
        // float minLeft = player.transform.position.x - 1f;
        // float minRight = player.transform.position.x + 1f;
        // float minLeft = direction.x - 1f;
        // float minRight = direction.x + 1f;
        float randx = Random.Range(direction.x - 1f, direction.x + 1f);
        float randy = Random.Range(direction.y - 1f, direction.y + 1f);
        float randz = Random.Range(direction.z - 1f, direction.z + 1f);
        laserList[currentLaser].GetComponent<Rigidbody>().velocity = new Vector3(randx, direction.y, direction.z).normalized * 10;
        // laserList[currentLaser].GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(minLeft, minRight), 0f, 0f)*5;
        // laserList[currentLaser ].GetComponent<Rigidbody>().AddForce(new Vector3(2f, 0f, 0f) * Time.deltaTime, ForceMode.Impulse);

        float rotX = Mathf.Atan2(direction.y, Mathf.Sqrt(randx * randx + direction.z * direction.z)) * Mathf.Rad2Deg;
        float rotY = Mathf.Atan2(-randx, -direction.z) * Mathf.Rad2Deg;
        float rotZ = 0f;
        laserList[currentLaser].transform.rotation = Quaternion.Euler(rotX - 90, rotY, rotZ);
        currentLaser++;
    }

    private IEnumerator AttackDelayCoroutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(2f); // Adjust the delay time as needed
        canAttack = true;
    }

    public void DamageMonster()
    {
        Debug.Log("Monster is attacked");
        currHp -= player.GetComponent<Player>().atk;
        Debug.Log($"Current Hp is {currHp}");
        if (currHp <= 0 )
        {
            Debug.Log("Monster Dead!");
            this.gameObject.SetActive(false);
            gameManager = GameObject.FindGameObjectWithTag("GameController");
            gameManager.GetComponent<GameManager>().Shopping();
        }
    }

}
