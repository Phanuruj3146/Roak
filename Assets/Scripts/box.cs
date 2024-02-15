using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.OpenXR.Input;

public class box : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<ParticleSystem>().Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.GetComponent<ParticleSystem>().Play();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            int val = Random.Range(0, 7);
            
            if (val == 0)
            {
                player.GetComponent<Player>().Heal();
            } else if (val == 1)
            {
                player.GetComponent<Player>().IncreaseScore();
            } else if (val == 2)
            {
                player.GetComponent<Player>().UpgradeHP();
            }
            else if (val == 3)
            {
                player.GetComponent<Player>().UpgradeATK();
            } else if(val == 4) 
            {
                player.GetComponent<Player>().UpgradeSPD();
            } else if (val == 5)
            {
                player.GetComponent<Player>().IncreaseLevel();
            } else
            {
                player.GetComponent<Player>().DamagePlayer(10);
            }
            StartCoroutine(DelaySpawn());
        }
    }

    private IEnumerator DelaySpawn()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(3f); // Adjust the delay time as needed
        this.gameObject.transform.position = new Vector3(Random.Range(this.gameObject.transform.position.x - 2, this.gameObject.transform.position.x + 4),
            Random.Range(this.gameObject.transform.position.y, this.gameObject.transform.position.y + 4),
            Random.Range(this.gameObject.transform.position.z - 2, this.gameObject.transform.position.z + 2))
        {

        };
        this.gameObject.GetComponent<Renderer>().enabled = true;
    }
}
