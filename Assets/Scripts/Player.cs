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
    public GameObject monster;
    public Rigidbody rigid;
    public CharInputSystem actionMap;
    [SerializeField] private Vector2 directionValue;
    [SerializeField] private float updown;
    // [SerializeField] private float forcePower = 1f;
    // Start is called before the first frame update
    void Start()
    {
        actionMap = new CharInputSystem();
        actionMap.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        directionValue = actionMap.Player.Movement.ReadValue<Vector2>();
        updown = actionMap.Player.UpDown.ReadValue<float>();

        Debug.Log(updown);
        // Make player focus enemy
        if (GameObject.FindGameObjectWithTag("Monster") != null)
        {
            monster = GameObject.FindGameObjectWithTag("Monster");
            this.transform.LookAt(monster.transform);
        }
    }

    private void FixedUpdate()
    {
        ApplyForce();
    }

    void ApplyForce()
    {
        Vector3 applyingForce = new Vector3 (directionValue[0] * spd, updown *  spd, directionValue[1] * spd);
        rigid.AddForce(applyingForce * Time.deltaTime, ForceMode.Impulse);
    }
}
