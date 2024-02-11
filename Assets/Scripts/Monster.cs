using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject player;
    public int hp;
    public int atk;
    public int atkCD;
    float currHp;
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        int playerLV = player.gameObject.GetComponent<Player>().lv;
        hp = 100 * playerLV;
        atk = 20 * playerLV;
        currHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
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
