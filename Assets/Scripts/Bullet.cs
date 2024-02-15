using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster") {
            if (GameObject.FindGameObjectWithTag("Monster") != null)
            {
                GameObject monster = GameObject.FindGameObjectWithTag("Monster");
                //GameObject player = GameObject.FindGameObjectWithTag("Player");
                monster.GetComponent<Monster>().DamageMonster(true);
                this.gameObject.SetActive(false);
            }
        }
    }
}
