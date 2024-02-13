using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public GameObject player;
    public GameObject parriedBomb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Laser")
        {
            Debug.Log("absorb laser");
        }
        if (other.gameObject.tag == "Bomb")
        {
            Debug.Log("absorb bomb");
            parriedBomb = Instantiate(parriedBomb);
            parriedBomb.transform.position = this.transform.position;
            parriedBomb.GetComponent<Rigidbody>().velocity = (this.transform.forward) * 10;
        }
    }
}
