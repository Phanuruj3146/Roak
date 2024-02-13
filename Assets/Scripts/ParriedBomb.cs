using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParriedBomb : MonoBehaviour
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
        if (other.gameObject.tag == "Monster")
        {
            if (GameObject.FindGameObjectWithTag("Monster") != null)
            {
                GameObject monster = GameObject.FindGameObjectWithTag("Monster");
                //GameObject player = GameObject.FindGameObjectWithTag("Player");
                monster.GetComponent<Monster>().DamageMonster();
                Destroy(this.gameObject);
            }
        }
    }
}
