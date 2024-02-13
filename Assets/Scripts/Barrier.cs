using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public GameObject player;
    public GameObject parriedBomb;
    public GameObject currParriedBomb;
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
            currParriedBomb = Instantiate(parriedBomb);
            currParriedBomb.transform.position = this.transform.position;
            currParriedBomb.GetComponent<Rigidbody>().velocity = (player.transform.up * -1f) * 10;
        }
    }
}
