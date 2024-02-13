using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR.Input;

public class box : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().Heal();
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
