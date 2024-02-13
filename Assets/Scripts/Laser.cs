using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
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
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Barrier")
        {
            this.gameObject.SetActive(false);
            GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
            gameManager.GetComponent<GameManager>().PlayerHit();
        }
    }
}
