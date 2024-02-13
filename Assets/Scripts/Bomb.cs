using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bomb : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rb;
    public float moveSpeed = 1f;
    public float initialSpeed = 1f;  // Initial speed of the bullet
    public float accelerationRate = 10f;
    // public float maxSpeed = 10f;

    private bool isActive = true;

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
        // Vector3 direction = player.transform.position - transform.position;
        // direction.Normalize();
        // moveCharacter(direction);
        if (isActive)
        {
            float exponentialSpeed = initialSpeed * Mathf.Pow(1 + accelerationRate, Time.time);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, exponentialSpeed * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, initialSpeed * Time.deltaTime);;
            // initialSpeed += accelerationRate * Time.deltaTime;
            // initialSpeed = Mathf.Min(initialSpeed, maxSpeed);
            print(initialSpeed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Barrier")
        {
            this.gameObject.SetActive(false);
            ResetBullet();
        }
    }

    private void ResetBullet()
    {
        // Reset the bullet's speed
        initialSpeed = 1f;

        // Set active to true to start following the player again
        isActive = true;
    }

    void moveCharacter(Vector3 directionOfTravel)
    {
        rb.MovePosition(transform.position + (directionOfTravel * moveSpeed * Time.time * Time.deltaTime));
    }
}
