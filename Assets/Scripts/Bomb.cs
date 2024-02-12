using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bomb : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 1f;
    private Rigidbody rb;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // Debug.Log(player.transform.position);
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        moveCharacter(direction);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    void moveCharacter(Vector3 directionOfTravel)
    {
        rb.MovePosition(transform.position + (directionOfTravel * moveSpeed * Time.deltaTime));
    }
}
