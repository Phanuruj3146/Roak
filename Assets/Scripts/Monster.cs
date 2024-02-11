using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject player;
    public int maxLaser = 5;
    public GameObject laser;
    public List<GameObject> laserList = new List<GameObject>();

    private bool canAttack = true;
    private int currentLaser = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxLaser; i++)
        {
            var newLaser = Instantiate(laser);
            newLaser.GetComponent<Renderer>().enabled = false;
            laserList.Add(newLaser);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Make monster focus player
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Vector3 look = transform.InverseTransformPoint(player.transform.position);
            float angle = Mathf.Atan2(look.y,look.x) * Mathf.Rad2Deg - 270;
            this.transform.Rotate(0,0,angle);
            //this.transform.LookAt(new Vector3 (player.transform.position.x,player.transform.position.y,player.transform.position.z));

        }
    }

    private void FixedUpdate()
    {
        if (canAttack)
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log(currentLaser);
        StartCoroutine(AttackDelayCoroutine());
        if (currentLaser == 5)
        {
            currentLaser = 0;
        }

        laserList[currentLaser].transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);
        laserList[currentLaser].GetComponent<Renderer>().enabled = true;
        laserList[currentLaser].GetComponent<Rigidbody>().velocity = transform.right * 5;
        currentLaser++;
    }

    private IEnumerator AttackDelayCoroutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(2f); // Adjust the delay time as needed
        canAttack = true;
    }

}
