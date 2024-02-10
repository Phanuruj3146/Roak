using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    public int hp = 100;
    public int atk = 10;
    public float spd = 10;
    public int parryGauge = 50;
    public int money = 0;
    public int maxBullet = 5;
    public GameObject monster;
    public Rigidbody rigid;
    public CharInputSystem actionMap;
    public GameObject bullet;
    public List<GameObject> bulletList = new List<GameObject>();

    private float isAttack;
    private bool canPress = true;
    private int currentBullet = 0;

    [SerializeField] private Vector2 directionValue;
    [SerializeField] private float updown;

    // [SerializeField] private float forcePower = 1f;
    // Start is called before the first frame update
    void Start()
    {
        actionMap = new CharInputSystem();
        actionMap.Enable();
        for (int i = 0; i < maxBullet; i++)
        {
            var newBullet = Instantiate(bullet);
            newBullet.GetComponent<Renderer>().enabled = false;
            bulletList.Add(newBullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        directionValue = actionMap.Player.Movement.ReadValue<Vector2>();
        updown = actionMap.Player.UpDown.ReadValue<float>();
        isAttack = actionMap.Player.Attack.ReadValue<float>();

        // Make player focus enemy
        if (GameObject.FindGameObjectWithTag("Monster") != null)
        {
            monster = GameObject.FindGameObjectWithTag("Monster");
            Vector3 look = transform.InverseTransformPoint(monster.transform.position);
            float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 270;
            this.transform.Rotate(0, 0, angle);
        }
    }

    private void FixedUpdate()
    {
        ApplyForce();
        if (isAttack == 1)
        {
            Attack();
        }
    }

    void ApplyForce()
    {
        Vector3 applyingForce = new Vector3 (directionValue[0] * spd, updown *  spd, directionValue[1] * spd);
        rigid.AddForce(applyingForce * Time.deltaTime, ForceMode.Impulse);
    }

    void Attack()
    {
        StartCoroutine(ButtonDelayCoroutine());
        if (currentBullet == 5)
        {
            currentBullet = 0;
        }
        Vector3 currentPos = this.transform.position;
        currentPos.x += 0.1f;
        currentPos.y -= 0.8f;

        bulletList[currentBullet].transform.position = this.transform.position;
        bulletList[currentBullet].GetComponent<Renderer>().enabled = true;
        bulletList[currentBullet].GetComponent<Rigidbody>().velocity = Vector3.zero;
        bulletList[currentBullet].transform.position = currentPos;
        currentBullet++;
    }

    // Upgrades
    public void UpgradeHP()
    {
        this.hp += 10;
    }

    public void UpgradeATK()
    {
        this.atk += 5;
    }

    public void UpgradeSPD()
    {
        this.spd += 5;
    }

    private IEnumerator ButtonDelayCoroutine()
    {
        // Disable button press
        canPress = false;

        // Wait for the delay (adjust the time accordingly)
        yield return new WaitForSeconds(0.5f); // Adjust the delay time as needed

        // Enable button press after the delay
        canPress = true;
    }
}
