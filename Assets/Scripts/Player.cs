using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 100;
    public int atk = 10;
    public float spd = 10;
    public GameObject monster;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Make player focus enemy
        if (GameObject.FindGameObjectWithTag("Monster") != null)
        {
            monster = GameObject.FindGameObjectWithTag("Monster");
            this.transform.LookAt(monster.transform);
        }
    }
}
