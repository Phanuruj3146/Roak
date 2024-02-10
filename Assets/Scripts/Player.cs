using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 100;
    public int atk = 10;
    public float spd = 10;
    public GameObject monster;
    public Rigidbody rigid;
    public CharInputSystem actionMap;
    [SerializeField] private Vector2 directionValue;
    [SerializeField] private float forcePower = 1f;
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
        Vector3 applyingForce = directionValue * forcePower;
        rigid.AddForce(applyingForce, ForceMode.Impulse);
    }
}
