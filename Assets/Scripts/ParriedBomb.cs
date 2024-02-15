using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ParriedBomb : MonoBehaviour
{
    public GameObject player;
    public GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        monster = GameObject.FindGameObjectWithTag("Monster");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, monster.transform.position, 10 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            if (GameObject.FindGameObjectWithTag("Monster") != null)
            {
                GameObject monster = GameObject.FindGameObjectWithTag("Monster");
                //GameObject player = GameObject.FindGameObjectWithTag("Player");
                monster.GetComponent<Monster>().DamageMonster(false);
                Destroy(this.gameObject);
            }
        }
    }
}
